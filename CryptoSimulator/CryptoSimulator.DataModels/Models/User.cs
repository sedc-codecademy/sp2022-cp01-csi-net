namespace CryptoSimulator.DataModels.Models
{
    public class User : BaseEntity
    {
        public User()
        {
            Wallet = new Wallet { MaxCoins = 10, Cash = 100_000};
            Transactions = new List<Transaction>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Wallet Wallet { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
