using AccountMgtApi.Models;
using MongoDB.Bson;

namespace AccountMgtApi.Services
{
    public interface IAggregationServices
    {
        List<Account> SearchAccountsLessThanBalance(decimal balance);
        List<Account> SearchAccountsGreaterThanBalance(decimal balance);
        List<GbpAccount> GroupAccountsByAccountType();
        List<GbpAccount> GroupAccountsByAccountType(decimal balance);        
        List<Account> SortAccountsByBalance(decimal balance);      
        List<GbpAccount> ProjectAccountResults(decimal balance);

        List<BsonDocument> MatchAccountsBsonDocument(decimal balance);
        List<BsonDocument> GroupAccountsByAccountTypeBsonDocument(decimal balance);
        List<BsonDocument> SortAccountsByBalanceBsonDocument(decimal balance);
    }
}
