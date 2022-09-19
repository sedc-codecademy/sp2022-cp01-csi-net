namespace CryptoSimulator.DataModels.Models
{
    public class Coin : BaseEntity
    {
        public string Name { get; set; }
        public List<int> PriceBought { get; set; }
        public int Quantity { get; set; }
    }
}
