using AccountMgtApi.Models;
using MongoDB.Driver;

namespace AccountMgtApi.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IMongoCollection<Account> accountsCollection;
        public AccountServices(IMongoCollection<Account> accountsCollection) 
        {
            this.accountsCollection = accountsCollection;
        }

        public DeleteResult deleteAccountByAccountId(string accountId)
        {
            return accountsCollection.DeleteOne(a => a.AccountId == accountId);
        }

        public async Task<DeleteResult> deleteAccountByAccountIdAsync(string accountId)
        {
            return await accountsCollection.DeleteOneAsync(a => a.AccountId == accountId);
        }

        public DeleteResult deleteAccountsByBalance(decimal balance)
        {
            return accountsCollection.DeleteMany(a => a.Balance < balance);
        }

        public async Task<DeleteResult> deleteAccountsByBalanceAsync(decimal balance)
        {
            return await accountsCollection.DeleteManyAsync(a => a.Balance <= balance);
        }

        public Account searchAccountByAccountId(string accountId)
        {
            return accountsCollection.Find(a => a.AccountId == accountId).FirstOrDefault();
        }

        public async Task<Account> searchAccountByAccountIdAsync(string accountId)
        {
            var accounts = await accountsCollection.FindAsync(a => a.AccountId == accountId);
            return accounts.FirstOrDefault();
        }

        public List<Account> searchAccountsByAcountType(string accountType)
        {
            return accountsCollection.Find(a => a.AccountType == accountType)
                .SortByDescending(a => a.Balance)
                .Skip(5) // skips the first 5 results
                .Limit(20) // limit the returned data set to next 20 items
                .ToList();
        }

        public List<Account> searchAllAccounts(string accountId)
        {
            return accountsCollection.Find(_ => true).ToList();
        }

        public UpdateResult updateAcountByAccountIdAndBalance(string accountId, decimal balance)
        {
            var filter = Builders<Account>.Filter.Eq(a => a.AccountId, accountId);
            var update = Builders<Account>.Update.Set(a => a.Balance, balance);

            return accountsCollection.UpdateOne(filter, update);
        }

        public async Task<UpdateResult> updateAcountByAccountIdAndBalanceAsync(string accountId, decimal balance)
        {
            var filter = Builders<Account>.Filter.Eq(a => a.AccountId, accountId);
            var update = Builders<Account>.Update.Set(a => a.Balance, balance);

            return await accountsCollection.UpdateOneAsync(filter, update);
        }

        public UpdateResult updateAcountsByAccountIdAndBalance(string accountId, decimal balance)
        {
            var filter = Builders<Account>.Filter.Eq(a => a.AccountId, accountId);
            var update = Builders<Account>.Update.Inc(a => a.Balance, balance);

            return accountsCollection.UpdateMany(filter, update);
        }
    }
}
