using CryptoSimulator.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSimulator.ServiceModels.WalletModels
{
    public class WalletDto
    {
        public int Id { get; set; }
        public double MaxCoins { get; set; }
        public double Cash { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Coin> Coins { get; set; }
        
    }
}
