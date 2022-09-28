namespace CryptoSimulator.DataModels.Models
{
    public class ActivityLog : BaseEntity
    {
        public int UserId { get; set; }
        public List<Transaction> Transactions { get; set; }
        public Transaction Transaction { get; set; }
    }
}
