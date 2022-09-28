namespace CryptoSimulator.DataModels.Models
{
    public class Coin : BaseEntity
    {
        public string CoinId { get; set; }   // id from coin gecko API
        public string Name { get; set; }
        //public List<double> PriceBought { get; set; }   // vaka ne dava da go kreiras vo db bidejki e lista ...
        // zatoa mozno resenie e da se stavi kako property DOUBLE PriceBought, i ponatamu vo logikata samo da se zgolemuva (+=) PriceBought na odreden coin (zaedno so quantity) za soodveten coin (koj ke se naogja po CoinId, a dokolku go nema ke se dodava soodvetno od apito)
        public double PriceBought { get; set; }
        public double Quantity { get; set; }
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; }
    }
}
