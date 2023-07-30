using AccountMgtApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AccountMgtApi.Services
{
    public class AggregationServices : IAggregationServices
    {
        IMongoCollection<Account> _accountsCollection;
        IMongoCollection<BsonDocument> _accountsCollectionBson;

        public AggregationServices(IMongoCollection<Account> accountsCollection, IMongoCollection<BsonDocument> accountsCollectionBson) 
        { 
            _accountsCollection = accountsCollection;
            _accountsCollectionBson = accountsCollectionBson;
        }

        public List<Account> matchAccounts(decimal balance)
        {
            var matchStage = Builders<Account>.Filter.Lte(a => a.Balance, balance);
            var aggregate = _accountsCollection.Aggregate().Match(matchStage);
            
            return aggregate.ToList();
        }

        public List<BsonDocument> matchAccountsBsonDocument(decimal balance)
        {
            var matchStage = Builders<BsonDocument>.Filter.Lte("balance", balance);
            var aggregate = _accountsCollectionBson.Aggregate().Match(matchStage);
            
            return aggregate.ToList();
        }

        public List<GbpAccount> groupAccountsByAcountType(decimal balance)
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

        public List<BsonDocument> groupAccountsByAcountTypeBsonDocument(decimal balance)
        {
            var matchStage = Builders<Account>.Filter.Lte(u => u.Balance, balance);
            var aggregate = _accountsCollection.Aggregate()
                                       .Match(matchStage)
                                       .Group(new BsonDocument { { "_id", "$account_type" }, { "AvgSum", new BsonDocument("$avg", "$balance") } });

            return aggregate.ToList();
        }

        public List<Account> sortAccountsByBalance(decimal balance)
        {
            var matchStage = Builders<Account>.Filter.Lt(u => u.Balance, balance);
            var aggregate = _accountsCollection.Aggregate()
                                       .Match(matchStage)
                                       .SortByDescending(a => a.Balance);
            return aggregate.ToList();
        }

        public List<BsonDocument> sortAccountsByBalanceBsonDocument(decimal balance)
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
