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
        List<Coin> GetAllCoins(int walletId);
        public int Insert(Coin entity);
        public void UpdateCoin(Coin entity);
        public Coin GetCoin(int walletId, string coinName);
        public List<Coin> GetAllCoinsInWallet(int walletId, string coinName);
        public int DeleteCoin(Coin coin);

    }
}
