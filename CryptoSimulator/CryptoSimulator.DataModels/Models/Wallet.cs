namespace CryptoSimulator.DataModels.Models
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public int MaxCoints { get; set; }
        public int Cash { get; set; }
        public int UserId { get; set; }
        public List<Coin> Coins { get; set; }
        public Coin Coin { get; set; }
        public string WalletAdress { get; set; }
    }
}
