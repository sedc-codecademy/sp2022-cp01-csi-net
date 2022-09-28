namespace CryptoSimulator.DataModels.Models
{
    public class Transaction : BaseEntity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public bool BuyOrSell { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get;  set; }
    }
}
