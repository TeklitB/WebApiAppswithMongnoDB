using MongoDB.Bson;
using MongoDB.Driver;

namespace AccountMgtApi.Services
{
    public class UseBsonDocumentServices : IUseBsonDocumentServices
    {
        private readonly IMongoCollection<BsonDocument> accountsCollection;

        public UseBsonDocumentServices(IMongoCollection<BsonDocument> accountsCollection) 
        {
            this.accountsCollection = accountsCollection;
        }

        public void addAccount()
        {
            var document = new BsonDocument
            {
               { "account_id", "MDB829001337" },
               { "account_holder", "Linus Torvalds" },
               { "account_type", "checking" },
               { "balance", 50352434 }
            };

            accountsCollection.InsertOne(document);           
        }

        public void AddAccounts()
        {
            var documents = new[]
            {
                new BsonDocument
                {
                    { "account_id", "MDB011235813" },
                    { "account_holder", "Ada Lovelace" },
                    { "account_type", "checking" },
                    { "balance", 60218 }
                },
                new BsonDocument
                {
                    { "account_id", "MDB829000001" },
                    { "account_holder", "Muhammad ibn Musa al-Khwarizmi" },
                    { "account_type", "savings" },
                    { "balance", 267914296 }
                }
            };

            accountsCollection.InsertMany(documents);
        }

        public BsonDocument searchAccount(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            
            return accountsCollection.Find(filter).FirstOrDefault();
        }

        public async Task<List<BsonDocument>> searchAccountAsync(decimal balance)
        {
            var filter = Builders<BsonDocument>.Filter.Gt("balance", balance);
            var documents = await accountsCollection.FindAsync(filter);

            return documents.ToList();
        }

        public UpdateResult updateAcount(decimal amount)
        {
            var filter = Builders<BsonDocument>.Filter.Lt("balance", 500);
            var update = Builders<BsonDocument>.Update.Inc("balance", amount);

            return accountsCollection.UpdateOne(filter, update);
        }

        public UpdateResult updateAcounts(decimal amount)
        {
            var filter = Builders<BsonDocument>.Filter.Lt("balance", 500);
            var update = Builders<BsonDocument>.Update.Inc("balance", amount);

            return accountsCollection.UpdateMany(filter, update);
        }

        public async Task<UpdateResult> updateAcountAsync(decimal amount)
        {
            var filter = Builders<BsonDocument>.Filter.Lt("balance", 500);
            var update = Builders<BsonDocument>.Update.Inc("balance", amount);

            return await accountsCollection.UpdateOneAsync(filter, update);
        }

        public async Task<UpdateResult> updateAcountsAsync(decimal amoumt)
        {
            var filter = Builders<BsonDocument>.Filter.Lt("balance", 500);
            var update = Builders<BsonDocument>.Update.Inc("balance", amoumt);

            return await accountsCollection.UpdateManyAsync(filter, update);
        }

        public DeleteResult deleteAccount(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            
            return accountsCollection.DeleteOne(filter);
        }

        public async Task<DeleteResult> deleteAccountAsync(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            
            return await accountsCollection.DeleteOneAsync(filter);
        }

        public DeleteResult deleteAccounts(string accountType)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("account_type", accountType);
            
            return accountsCollection.DeleteMany(filter);
        }

        public async Task<DeleteResult> deleteAccountsAsync(string accountType)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("account_type", accountType);
            
            return await accountsCollection.DeleteManyAsync(filter);
        }
    }
}
