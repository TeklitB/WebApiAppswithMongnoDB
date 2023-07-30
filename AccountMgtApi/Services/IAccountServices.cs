using AccountMgtApi.Models;
using MongoDB.Driver;

namespace AccountMgtApi.Services
{
    public interface IAccountServices
    {
        List<Account> SearchAllAccounts();
        Account SearchAccountByAccountId(string accountId);
        Task<Account> searchAccountByAccountIdAsync(string accountId);
        List<Account> SearchAccountsByAcountType(string accountType);
        UpdateResult UpdateBalanceByAccountId(string accountId, decimal balance);
        Task<UpdateResult> UpdateBalanceByAccountIdAsync(string accountId, decimal balance);
        UpdateResult UpdateBalancesByAccountId(string accountId, decimal balance);
        DeleteResult deleteAccountByAccountId(string accountId);
        Task<DeleteResult> deleteAccountByAccountIdAsync(string accountId);
        DeleteResult deleteAccountsByBalance(decimal balance);
        Task<DeleteResult> deleteAccountsByBalanceAsync(decimal balance);
    }
}
