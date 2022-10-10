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
    public class CoinRepository : BaseRepository , ICoinRepository
    {
        public CoinRepository(CryptoSimulatorDbContext context) : base(context)
        {
        }


        public List<Coin> GetAllCoinsInWallet(int walletId, string coinName)
        {
            var coinList = new List<Coin>();
            
               var ifCoinListExists = _context.Coins.Where(x => x.WalletId == walletId && x.Name == coinName).ToList();
              if(ifCoinListExists != null || ifCoinListExists.Count > 0)
                {
                    return ifCoinListExists;
                }
            return coinList;
        }

        public Coin GetById(int id)
        {
            return _context.Coins.FirstOrDefault(x => x.Id == id);
        }
        public Coin GetCoin (int walletId,string coinName)
        {
            return _context.Coins.FirstOrDefault(x =>x.WalletId == walletId && x.Name == coinName);
        }

        public Coin GetCoinByName(string name)
        {
            return _context.Coins.FirstOrDefault(x => x.Name == name);
        }


        public int Insert(Coin entity)
        {
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.[Coins] ON;");
            _context.Coins.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public void UpdateCoin(Coin entiry)
        {
           
            _context.Coins.Update(entiry);
            _context.SaveChanges();
            
        }
        public int DeleteCoin(Coin coin)
        {
            _context.Coins.Remove(coin);
            _context.SaveChanges();
            return coin.Id;
        }
    }
}
