using AccountMgtApi.Models;
using MongoDB.Driver;

namespace AccountMgtApi.Services
{
    public interface IAccountServices
    {
        List<Account> SearchAllAccounts();
        Account SearchAccountByAccountId(string accountId);
        Task<Account> SearchAccountByAccountIdAsync(string accountId);
        List<Account> SearchAccountsByAccountType(string accountType);
        UpdateResult UpdateBalanceByAccountId(string accountId, decimal balance);
        Task<UpdateResult> UpdateBalanceByAccountIdAsync(string accountId, decimal balance);
        UpdateResult UpdateBalancesByAccountId(string accountId, decimal balance);
        DeleteResult DeleteAccountByAccountId(string accountId);
        Task<DeleteResult> DeleteAccountByAccountIdAsync(string accountId);
        DeleteResult DeleteAccountsByBalance(decimal balance);
        Task<DeleteResult> DeleteAccountsByBalanceAsync(decimal balance);
    }
}
