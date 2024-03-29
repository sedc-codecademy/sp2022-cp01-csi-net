﻿using CryptoSimulator.DataModels.Models;
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

        public IEnumerable<Transaction> GetAllUserTransactions(int userId);

        public List<Transaction> GetAllUserTransactionsCoinName(int userId, string coinName);
    }
}
