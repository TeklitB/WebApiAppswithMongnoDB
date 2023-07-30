namespace AccountMgtApi.Services
{
    public interface ITransactionServices
    {
        void PerformeTransfer(string fromId, string toId, string transferId, decimal transferAmount);
        Task PerformeTransactionsAsync(string fromId, string toId, string transferId, decimal transferAmount);
    }
}
