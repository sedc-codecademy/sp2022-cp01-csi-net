using CryptoSimulator.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSimulator.DataAccess.Repositories.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        public Transaction GetById(int id);
        public void Insert(Transaction transaction);

        public List<Transaction> GetAllUserTransactions(int userId);
    }
}
