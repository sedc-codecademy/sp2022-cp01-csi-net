using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSimulator.ServiceModels.WalletModels
{
    public class BuyOrSellCoinModel
    {
        public string CoinId { get; set; }   // id from coin gecko API
        public string Name { get; set; }
        public double PriceSold { get; set; }
        public double Quantity { get; set; }
    }
}
