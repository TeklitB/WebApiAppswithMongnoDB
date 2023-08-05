using BlogMgtApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlogMgtApi.Repository
{
    public class UsersRepository : IUsersRepository
    {
        //private IMongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<User> _usersCollection;

        public UsersRepository(IBlogSettings settings, IMongoClient client)
        {
            client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
            _usersCollection = _database.GetCollection<User>(settings.UsersCollectionName);
        }

        public async Task InsertUser(User user)
        {
            await _usersCollection.InsertOneAsync(user);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _usersCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<List<User>> GetUsersByField(string fieldName, string fieldValue)
        {
            var filter = Builders<User>.Filter.Eq(fieldName, fieldValue);
            var result = await _usersCollection.Find(filter).ToListAsync();

            return result;
        }

        public async Task<List<User>> GetUsers(int startingFrom, int count)
        {
            var result = await _usersCollection.Find(new BsonDocument())
                                               .Skip(startingFrom)
                                               .Limit(count)
                                               .ToListAsync();

            return result;
        }

        public async Task<bool> UpdateUser(ObjectId id, string udateFieldName, string updateFieldValue)
        {
            var filter = Builders<User>.Filter.Eq("_id", id);
            var update = Builders<User>.Update.Set(udateFieldName, updateFieldValue);

            var result = await _usersCollection.UpdateOneAsync(filter, update);

            return result.ModifiedCount != 0;
        }

        public async Task<bool> DeleteUserById(ObjectId id)
        {
            var filter = Builders<User>.Filter.Eq("_id", id);
            var result = await _usersCollection.DeleteOneAsync(filter);
            return result.DeletedCount != 0;
        }

        public async Task<long> DeleteAllUsers()
        {
            var filter = new BsonDocument();
            var result = await _usersCollection.DeleteManyAsync(filter);
            return result.DeletedCount;
        }

        public async Task CreateIndexOnCollection(IMongoCollection<BsonDocument> collection, string field)
        {
            var keys = Builders<BsonDocument>.IndexKeys.Ascending(field);
            await collection.Indexes.CreateOneAsync(keys);
        }

        public async Task CreateIndexOnNameField()
        {
            var keys = Builders<User>.IndexKeys.Ascending(x => x.Name);
            await _usersCollection.Indexes.CreateOneAsync(keys);
        }
    }
}
