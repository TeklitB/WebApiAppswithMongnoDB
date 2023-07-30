namespace AccountMgtApi.Models
{
    public class GbpAccount
    {
        public string AccountId { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public decimal GBP { get; set; }
        public int Total { get; set; }
    }
}
