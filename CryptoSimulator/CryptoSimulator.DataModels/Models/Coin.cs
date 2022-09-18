using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CryptoSimulator.DataModels.Models
{
    public class Coin
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<int> PriceBought { get; set; }
        public int Quantity { get; set; }
    }
}
