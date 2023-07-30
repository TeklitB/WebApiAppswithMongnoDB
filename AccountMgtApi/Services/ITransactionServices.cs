namespace AccountMgtApi.Services
{
    public interface ITransactionServices
    {
        void PerformeTransactions(string fromId, string toId, string transferId, int transferAmount);
        Task PerformeTransactionsAsync(string fromId, string toId, string transferId, int transferAmount);
    }
}
