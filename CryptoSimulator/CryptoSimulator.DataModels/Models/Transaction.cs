using CryptoSimulator.DataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSimulator.DataModels.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool BuyOrSell { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get { return TotalPrice; } protected set { TotalPrice = Quantity * Price;} } 

        

    }
}
