using AccountMgtApi.Models;
using MongoDB.Driver;

namespace AccountMgtApi.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IMongoCollection<Account> _accountsCollection;

        public AccountServices(IBankSettings bankSettings, IMongoClient mongoClient) 
        {
            var database = mongoClient.GetDatabase(bankSettings.DatabaseName);
            _accountsCollection = database.GetCollection<Account>(bankSettings.AccountCollectionName);
        }

        public DeleteResult deleteAccountByAccountId(string accountId)
        {
            return _accountsCollection.DeleteOne(a => a.AccountId == accountId);
        }

        public async Task<DeleteResult> deleteAccountByAccountIdAsync(string accountId)
        {
            return await _accountsCollection.DeleteOneAsync(a => a.AccountId == accountId);
        }

        public DeleteResult deleteAccountsByBalance(decimal balance)
        {
            return _accountsCollection.DeleteMany(a => a.Balance < balance);
        }

        public async Task<DeleteResult> deleteAccountsByBalanceAsync(decimal balance)
        {
            return await _accountsCollection.DeleteManyAsync(a => a.Balance <= balance);
        }

        public Account SearchAccountByAccountId(string accountId)
        {
            return _accountsCollection.Find(a => a.AccountId == accountId).FirstOrDefault();
        }

        public async Task<Account> searchAccountByAccountIdAsync(string accountId)
        {
            var accounts = await _accountsCollection.FindAsync(a => a.AccountId == accountId);
            return accounts.FirstOrDefault();
        }

        public List<Account> SearchAccountsByAcountType(string accountType)
        {
            return _accountsCollection.Find(a => a.AccountType == accountType)
                .SortByDescending(a => a.Balance)
                //.Skip(5) // skips the first 5 results
                .Limit(20) // limit the returned data set to next 20 items
                .ToList();
        }

        public List<Account> SearchAllAccounts()
        {
            return _accountsCollection.Find(_ => true).ToList();
        }

        public UpdateResult UpdateBalanceByAccountId(string accountId, decimal balance)
        {
            var filter = Builders<Account>.Filter.Eq(a => a.AccountId, accountId);
            var update = Builders<Account>.Update.Set(a => a.Balance, balance);

            return _accountsCollection.UpdateOne(filter, update);
        }

        public async Task<UpdateResult> UpdateBalanceByAccountIdAsync(string accountId, decimal balance)
        {
            var filter = Builders<Account>.Filter.Eq(a => a.AccountId, accountId);
            var update = Builders<Account>.Update.Set(a => a.Balance, balance);

            return await _accountsCollection.UpdateOneAsync(filter, update);
        }

        public UpdateResult UpdateBalancesByAccountId(string accountId, decimal balance)
        {
            var filter = Builders<Account>.Filter.Eq(a => a.AccountId, accountId);
            var update = Builders<Account>.Update.Inc(a => a.Balance, balance);

            return _accountsCollection.UpdateMany(filter, update);
        }
    }
}
