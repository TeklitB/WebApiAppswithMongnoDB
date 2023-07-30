using AccountMgtApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AccountMgtApi.Services
{
    public class UseBsonDocumentServices : IUseBsonDocumentServices
    {
        private readonly IMongoCollection<BsonDocument> _accountsCollection;

        public UseBsonDocumentServices(IBankSettings bankSettings, IMongoClient mongoClient) 
        {
            var database = mongoClient.GetDatabase(bankSettings.DatabaseName);
            _accountsCollection = database.GetCollection<BsonDocument>(bankSettings.AccountCollectionName);
        }

        public void AddAccount()
        {
            var document = new BsonDocument
            {
               { "account_id", "MDB829001338" },
               { "account_holder", "Bill Gates" },
               { "account_type", "savings" },
               { "balance", 5000 }
            };

            _accountsCollection.InsertOne(document);           
        }

        public void AddAccounts()
        {
            var documents = new[]
            {
                new BsonDocument
                {
                    { "account_id", "MDB829001339" },
                    { "account_holder", "Stev Jobs" },
                    { "account_type", "checking" },
                    { "balance", 6000 }
                },
                new BsonDocument
                {
                    { "account_id", "MDB829001340" },
                    { "account_holder", "Thomas Jefferson" },
                    { "account_type", "savings" },
                    { "balance", 2000 }
                }
            };

            _accountsCollection.InsertMany(documents);
        }

        public BsonDocument searchAccount(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            
            return _accountsCollection.Find(filter).FirstOrDefault();
        }

        public async Task<List<BsonDocument>> searchAccountAsync(decimal balance)
        {
            var filter = Builders<BsonDocument>.Filter.Gt("balance", balance);
            var documents = await _accountsCollection.FindAsync(filter);

            return documents.ToList();
        }

        public UpdateResult updateAcount(decimal amount)
        {
            var filter = Builders<BsonDocument>.Filter.Lt("balance", 500);
            var update = Builders<BsonDocument>.Update.Inc("balance", amount);

            return _accountsCollection.UpdateOne(filter, update);
        }

        public UpdateResult updateAcounts(decimal amount)
        {
            var filter = Builders<BsonDocument>.Filter.Lt("balance", 500);
            var update = Builders<BsonDocument>.Update.Inc("balance", amount);

            return _accountsCollection.UpdateMany(filter, update);
        }

        public async Task<UpdateResult> updateAcountAsync(decimal amount)
        {
            var filter = Builders<BsonDocument>.Filter.Lt("balance", 500);
            var update = Builders<BsonDocument>.Update.Inc("balance", amount);

            return await _accountsCollection.UpdateOneAsync(filter, update);
        }

        public async Task<UpdateResult> updateAcountsAsync(decimal amoumt)
        {
            var filter = Builders<BsonDocument>.Filter.Lt("balance", 500);
            var update = Builders<BsonDocument>.Update.Inc("balance", amoumt);

            return await _accountsCollection.UpdateManyAsync(filter, update);
        }

        public DeleteResult deleteAccount(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            
            return _accountsCollection.DeleteOne(filter);
        }

        public async Task<DeleteResult> deleteAccountAsync(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            
            return await _accountsCollection.DeleteOneAsync(filter);
        }

        public DeleteResult deleteAccounts(string accountType)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("account_type", accountType);
            
            return _accountsCollection.DeleteMany(filter);
        }

        public async Task<DeleteResult> deleteAccountsAsync(string accountType)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("account_type", accountType);
            
            return await _accountsCollection.DeleteManyAsync(filter);
        }
    }
}
