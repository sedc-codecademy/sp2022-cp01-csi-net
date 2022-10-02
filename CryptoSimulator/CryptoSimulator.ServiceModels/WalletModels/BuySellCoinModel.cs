using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSimulator.ServiceModels.WalletModels
{
    public class BuySellCoinModel
    {
        public string CoinId { get; set; }   // id from coin gecko API
        public string Name { get; set; }
        public int UserId { get; set; }
        public double Amount { get; set; }
    }
}
