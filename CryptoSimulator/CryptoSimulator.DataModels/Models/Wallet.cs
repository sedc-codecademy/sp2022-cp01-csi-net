namespace CryptoSimulator.DataModels.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public int MaxCoins { get; set; }
        public double Cash { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Coin> Coins { get; set; }

        public Wallet()
        {
            Coins = new List<Coin>();
        }
    }
}
