using MongoDB.Bson;
using MongoDB.Driver;

namespace AccountMgtApi.Services
{
    public interface IUseBsonDocumentServices
    {
        void addAccount();
        void AddAccounts();
        BsonDocument searchAccount(string id);
        Task<List<BsonDocument>> searchAccountAsync(decimal balance);
        UpdateResult updateAcount(decimal amount);
        UpdateResult updateAcounts(decimal amount);
        Task<UpdateResult> updateAcountAsync(decimal amount);
        Task<UpdateResult> updateAcountsAsync(decimal amoumt);
        DeleteResult deleteAccount(string id);
        Task<DeleteResult> deleteAccountAsync(string id);
        DeleteResult deleteAccounts(string accountType);
        Task<DeleteResult> deleteAccountsAsync(string accountType);
    }
}
