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
    public class CoinRepository : BaseRepository, ICoinRepository
    {
        public CoinRepository(CryptoSimulatorDbContext context) : base(context)
        {
        }

        public List<Coin> GetAllCoinsInWallet(int walletId)
        {
            try
            {
                return _context.Coins.Where(x => x.WalletId == walletId).ToList();
            }
            catch (Exception)
            {
                return new List<Coin>();
            }
        }

        public Coin GetById(int id)
        {
            return _context.Coins.FirstOrDefault(x => x.Id == id);
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
    }
}
