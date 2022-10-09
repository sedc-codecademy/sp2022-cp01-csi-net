using CryptoSimulator.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSimulator.DataAccess.Repositories.Interfaces
{
    public interface ICoinRepository : IRepository<Coin>
    {
        public Coin GetById(int id);
        public int Insert(Coin entity);

        public List<Coin> GetAllCoinsInWallet(int walletId);
        
    }
}
