using BlogMgtApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlogMgtApi.Repository
{
    public interface IUsersRepository
    {
        Task InsertUser(User user);
        Task<List<User>> GetAllUsers();
        Task<List<User>> GetUsersByField(string fieldName, string fieldValue);
        Task<List<User>> GetUsers(int startingFrom, int count);
        Task<bool> UpdateUser(ObjectId id, string udateFieldName, string updateFieldValue);
        Task<bool> DeleteUserById(ObjectId id);
        Task<long> DeleteAllUsers();
        Task CreateIndexOnCollection(IMongoCollection<BsonDocument> collection, string field);
        Task CreateIndexOnNameField();
    }
}
