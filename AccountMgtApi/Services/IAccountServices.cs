using AccountMgtApi.Models;
using MongoDB.Driver;

namespace AccountMgtApi.Services
{
    public interface IAccountServices
    {
        List<Account> searchAllAccounts(string accountId);
        Account searchAccountByAccountId(string accountId);
        Task<Account> searchAccountByAccountIdAsync(string accountId);
        List<Account> searchAccountsByAcountType(string accountType);
        UpdateResult updateAcountByAccountIdAndBalance(string accountId, decimal balance);
        Task<UpdateResult> updateAcountByAccountIdAndBalanceAsync(string accountId, decimal balance);
        UpdateResult updateAcountsByAccountIdAndBalance(string accountId, decimal balance);
        DeleteResult deleteAccountByAccountId(string accountId);
        Task<DeleteResult> deleteAccountByAccountIdAsync(string accountId);
        DeleteResult deleteAccountsByBalance(decimal balance);
        Task<DeleteResult> deleteAccountsByBalanceAsync(decimal balance);
    }
}
