namespace AccountMgtApi.Models
{
    public class BankSettings : IBankSettings
    {
        public string DatabaseName { get; set; }
        public string AccountCollectionName { get; set; }
        public string TransactionCollectionName { get; set; }
        public string ConnectionString { get; set; }
    }
}
