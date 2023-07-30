namespace AccountMgtApi.Models
{
    public interface IBankSettings
    {
        string DatabaseName { get; set; }
        string AccountCollectionName { get; set; }
        string TransactionCollectionName { get; set; }
        string ConnectionString { get; set; }
    }
}
