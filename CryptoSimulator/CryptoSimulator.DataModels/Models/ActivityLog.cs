using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CryptoSimulator.DataModels.Models
{
    public class ActivityLog
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<Transaction> Transactions { get; set; }
        public Transaction Transaction { get; set; }
    }
}
