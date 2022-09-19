using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSimulator.DataModels.Models
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public int MaxCoints { get; set; } = 10;
        public int Cash { get; set; } = 100000;
        public Guid UserId { get; set; }
        public List<Coin> Coins { get; set; }
        public Coin Coin { get; set; }
        public string WalletAdress { get; set; }
    }
}
