using AccountMgtApi.Models;
using MongoDB.Bson;

namespace AccountMgtApi.Services
{
    public interface IAggregationServices
    {
        List<Account> matchAccounts(decimal balance);
        List<BsonDocument> matchAccountsBsonDocument(decimal balance);
        List<GbpAccount> groupAccountsByAcountType(decimal balance);
        List<BsonDocument> groupAccountsByAcountTypeBsonDocument(decimal balance);
        List<Account> sortAccountsByBalance(decimal balance);
        List<BsonDocument> sortAccountsByBalanceBsonDocument(decimal balance);
        List<GbpAccount> ProjectAccountResults(decimal balance);
    }
}
