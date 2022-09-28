namespace CryptoSimulator.DataModels.Models
{
    public class Transaction : BaseEntity
    {
        public string CoinName { get; set; }
        public double Price { get; set; }
        // true == Buy , false == Sell
        public bool BuyOrSell { get; set; }
        public double Quantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
