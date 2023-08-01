using AccountMgtApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AccountMgtApi.Services
{
    public class AggregationServices : IAggregationServices
    {
        IMongoCollection<Account> _accountsCollection;
        IMongoCollection<BsonDocument> _accountsCollectionBson;

        public AggregationServices(IBankSettings bankSettings, IMongoClient mongoClient) 
        {
            var database = mongoClient.GetDatabase(bankSettings.DatabaseName);
            _accountsCollection = database.GetCollection<Account>(bankSettings.AccountCollectionName);
            _accountsCollectionBson = database.GetCollection<BsonDocument>(bankSettings.AccountCollectionName);
        }

        public List<Account> SearchAccountsLessThanBalance(decimal balance)
        {
            var matchStage = Builders<Account>.Filter.Lte(a => a.Balance, balance);
            var aggregate = _accountsCollection.Aggregate().Match(matchStage);
            
            return aggregate.ToList();
        }

        public List<Account> SearchAccountsGreaterThanBalance(decimal balance)
        {
            var matchStage = Builders<Account>.Filter.Gte(a => a.Balance, balance);
            var aggregate = _accountsCollection.Aggregate().Match(matchStage);

            return aggregate.ToList();
        }

        public List<BsonDocument> MatchAccountsBsonDocument(decimal balance)
        {
            var matchStage = Builders<BsonDocument>.Filter.Lte("balance", balance);
            var aggregate = _accountsCollectionBson.Aggregate().Match(matchStage);
            
            return aggregate.ToList();
        }

        public List<GbpAccount> GroupAccountsByAccountType(decimal balance)
        {
            var matchStage = Builders<Account>.Filter.Lte(a => a.Balance, balance);

            var aggregate = _accountsCollection.Aggregate()
                .Match(matchStage)
                .Group(a => a.AccountType, r => new GbpAccount
                {
                    AccountType = r.Key,
                    Total = r.Sum(a => 1)
                });
            
            return aggregate.ToList();
        }

        public List<GbpAccount> GroupAccountsByAccountType()
        {
            var aggregate = _accountsCollection.Aggregate()
                .Group(a => a.AccountType, r => new GbpAccount
                {
                    AccountType = r.Key,
                    Total = r.Sum(a => 1)
                });

            return aggregate.ToList();
        }

        public List<BsonDocument> GroupAccountsByAccountTypeBsonDocument(decimal balance)
        {
            var matchStage = Builders<Account>.Filter.Lte(u => u.Balance, balance);
            var aggregate = _accountsCollection.Aggregate()
                                       .Match(matchStage)
                                       .Group(new BsonDocument { { "_id", "$account_type" }, { "AvgSum", new BsonDocument("$avg", "$balance") } });

            return aggregate.ToList();
        }

        public List<Account> SortAccountsByBalance(decimal balance)
        {
            var matchStage = Builders<Account>.Filter.Lt(u => u.Balance, balance);
            var aggregate = _accountsCollection.Aggregate()
                                       .Match(matchStage)
                                       .SortByDescending(a => a.Balance);
            return aggregate.ToList();
        }

        public List<BsonDocument> SortAccountsByBalanceBsonDocument(decimal balance)
        {
            var matchStage = Builders<BsonDocument>.Filter.Lt("balance", balance);
            var sort = Builders<BsonDocument>.Sort.Descending("balance");

            var aggregate = _accountsCollectionBson.Aggregate()
                                       .Match(matchStage)
                                       .Sort(sort);
            
            return aggregate.ToList();
        }

        public List<GbpAccount> ProjectAccountResults(decimal balance)
        {
            var matchStage = Builders<Account>.Filter.Lt(u => u.Balance, balance);
            var projectStage = Builders<Account>.Projection.Expression(u => new GbpAccount
            {
                AccountId = u.AccountId,
                AccountType = u.AccountType,
                Balance = u.Balance,
                GBP = u.Balance / 1.30M
            });

            var aggregate = _accountsCollection.Aggregate()
                .Match(matchStage)
                .SortByDescending(u => u.Balance)
                .Project(projectStage);

            return aggregate.ToList();
        }
    }
}
