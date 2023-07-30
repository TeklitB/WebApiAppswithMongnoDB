using AccountMgtApi.Models;
using MongoDB.Driver;

namespace AccountMgtApi.Services
{
    public class TransactionServices : ITransactionServices
    {
        private readonly MongoClient _client;
        private readonly IMongoCollection<Account> _accountsCollection;
        private readonly IMongoCollection<Transfer> _transfersCollection;

        public TransactionServices(MongoClient client, IMongoCollection<Account> accountsCollection, IMongoCollection<Transfer> transfersCollection) 
        { 
            _client = client;
            _accountsCollection = accountsCollection;
            _transfersCollection = transfersCollection;
        }

        public void PerformeTransactions(string fromId, string toId, string transferId, int transferAmount)
        {
            using (var session = _client.StartSession())
            {

                // Define the sequence of operations to perform inside the transactions
                session.WithTransaction(
                    (s, ct) =>
                    {
                        // Obtain the account that the money will be coming from
                        var fromAccountResult = _accountsCollection.Find(e => e.AccountId == fromId).FirstOrDefault();
                        // Get the balance and id of the account that the money will be coming from
                        var fromAccountBalance = fromAccountResult.Balance - transferAmount;
                        var fromAccountId = fromAccountResult.AccountId;

                        Console.WriteLine(fromAccountBalance.GetType());

                        // Obtain the account that the money will be going to
                        var toAccountResult = _accountsCollection.Find(e => e.AccountId == toId).FirstOrDefault();
                        // Get the balance and id of the account that the money will be going to
                        var toAccountBalance = toAccountResult.Balance + transferAmount;
                        var toAccountId = toAccountResult.AccountId;

                        // Create the transfer record
                        var transferDocument = new Transfer
                        {
                            TransferId = transferId,
                            ToAccount = toAccountId,
                            FromAccount = fromAccountId,
                            Amount = transferAmount
                        };

                        // Update the balance and transfer array for each account
                        var fromAccountUpdateBalance = Builders<Account>.Update.Set(a => a.Balance, fromAccountBalance);
                        var fromAccountFilter = Builders<Account>.Filter.Eq(a => a.AccountId, fromId);
                        _accountsCollection.UpdateOne(fromAccountFilter, fromAccountUpdateBalance);

                        var fromAccountUpdateTransfers = Builders<Account>.Update.Push(a => a.TransfersCompleted, transferId);
                        _accountsCollection.UpdateOne(fromAccountFilter, fromAccountUpdateTransfers);

                        var toAccountUpdateBalance = Builders<Account>.Update.Set(a => a.Balance, toAccountBalance);
                        var toAccountFilter = Builders<Account>.Filter.Eq(a => a.AccountId, toId);
                        _accountsCollection.UpdateOne(toAccountFilter, toAccountUpdateBalance);
                        var toAccountUpdateTransfers = Builders<Account>.Update.Push(a => a.TransfersCompleted, transferId);

                        // Insert transfer doc
                        _transfersCollection.InsertOne(transferDocument);
                        Console.WriteLine("Transaction complete!");
                        return "Inserted into collections in different databases";
                    });
            }
        }

        public async Task PerformeTransactionsAsync(string fromId, string toId, string transferId, int transferAmount)
        {
            using (var session = await _client.StartSessionAsync())
            {
                try
                {
                    // TODO: Replace `<method>` with your answer below:
                    await session.WithTransactionAsync(
                        async (s, ct) =>
                        {
                            // Obtain the account the money will be coming from
                            var fromAccountResult = await _accountsCollection.Find(e => e.AccountId == fromId).FirstOrDefaultAsync();
                            // Get the balance and id that the money will be coming from
                            var fromAccountBalance = fromAccountResult.Balance - transferAmount;
                            var fromAccountId = fromAccountResult.AccountId;

                            // Obtain the account the money will be going
                            var toAccountResult = await _accountsCollection.Find(e => e.AccountId == toId).FirstOrDefaultAsync();
                            // Get the balance and id that the money will be going too
                            var toAccountBalance = toAccountResult.Balance + transferAmount;
                            var toAccountId = toAccountResult.AccountId;

                            // Create the transfer record
                            var transferDocument = new Transfer
                            {
                                TransferId = transferId,
                                ToAccount = toAccountId,
                                FromAccount = fromAccountId,
                                Amount = transferAmount,
                                LastUpdated = DateTimeOffset.UtcNow
                            };

                            // Update the balance and transfer array for each account
                            var fromAccountUpdateBalance = Builders<Account>.Update.Set("balance", fromAccountBalance);
                            var fromAccountFilter = Builders<Account>.Filter.Eq("account_id", fromId);
                            await _accountsCollection.UpdateOneAsync(fromAccountFilter, fromAccountUpdateBalance);

                            var fromAccountUpdateTransfers = Builders<Account>.Update.Push("transfers_complete", transferId);
                            await _accountsCollection.UpdateOneAsync(fromAccountFilter, fromAccountUpdateTransfers);

                            var toAccountUpdateBalance = Builders<Account>.Update.Set("balance", toAccountBalance);
                            var toAccountFilter = Builders<Account>.Filter.Eq("account_id", toId);
                            await _accountsCollection.UpdateOneAsync(toAccountFilter, toAccountUpdateBalance);
                            var toAccountUpdateTransfers = Builders<Account>.Update.Push("transfers_complete", transferId);
                            await _accountsCollection.UpdateOneAsync(toAccountFilter, toAccountUpdateTransfers);

                            // Insert transfer doc
                            await _transfersCollection.InsertOneAsync(transferDocument);
                            Console.WriteLine("Transaction complete!");
                            return "Inserted into collections in different databases";
                        });
                }
                catch
                {
                    await session.AbortTransactionAsync(); // now Dispose on the session has nothing to do and won't block
                    throw;
                }
                await session.CommitTransactionAsync();
            }
        }
    }
}
