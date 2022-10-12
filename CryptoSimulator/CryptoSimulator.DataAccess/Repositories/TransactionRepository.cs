using CryptoSimulator.DataAccess.Data;
using CryptoSimulator.DataAccess.Repositories.Interfaces;
using CryptoSimulator.DataModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSimulator.DataAccess.Repositories
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        public TransactionRepository(CryptoSimulatorDbContext context) : base(context)
        {
        }

        public IEnumerable<Transaction> GetAllUserTransactions(int userId)
        {
            return _context.UserTransactions.Where(x => x.UserId == userId);      
        }

        public List<Transaction> GetAllUserTransactionsCoinName(int userId, string coinName)
        {
            var transactionList = new List<Transaction>();

            var transactionListExists = _context.UserTransactions.Where(x => x.UserId == userId && x.CoinName == coinName);
            if (transactionList != null)
            {
                return transactionListExists.ToList();
            }
            return transactionList;
        }

        public Transaction GetById(int id)
        {
            return _context.UserTransactions.SingleOrDefault(x => x.Id == id);
        }


        public void Insert(Transaction entity)
        {
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.UserTransactions ON;");
            _context.UserTransactions.Add(entity);
            _context.SaveChanges();

        }
    }
}
