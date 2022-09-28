namespace CryptoSimulator.DataModels.Models
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public double MaxCoins { get; set; }
        public double Cash { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Coin> Coins { get; set; }
    }
}
