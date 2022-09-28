namespace CryptoSimulator.DataModels.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public int ActivityLogId { get; set; }
        public ActivityLog ActivityLog { get; set; }
    }
}
