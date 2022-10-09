using CryptoSimulator.DataAccess.Data;
using CryptoSimulator.DataAccess.Repositories.Interfaces;
using CryptoSimulator.DataModels.Models;
using CryptoSimulator.ServiceModels.WalletModels;
using Microsoft.EntityFrameworkCore;

namespace CryptoSimulator.DataAccess.Repositories
{
    public class WalletRepository : BaseRepository, IWalletRepository
    {
        
        public WalletRepository(CryptoSimulatorDbContext context) : base(context)
        {

        }

        public Wallet GetById(int id)
        {
            return _context.Wallets.SingleOrDefault(w => w.Id == id);
           
        }

        public Wallet GetByUserId(int userId)
        {
            var wallet = _context.Wallets.FirstOrDefault(x => x.UserId == userId);
           
            if (wallet != null)
            {
                return wallet;
            }
           return wallet = new Wallet();
        }

        public List<string> GetCoinIDsList()
        {
            var coinIdsList = new List<string>();
            // Here we should get list of the coingecko ids of all the coins we have in the wallet so that we can send a new request to get the current price 
            return coinIdsList;
        }

        // We may not need this method

        //public IEnumerable<Coin> GetAllCoins(int walletId)
        //{
        //    return _context.Coins.Where(c => c.WalletId == walletId);
        //}

        public void AddCoin()
        {

        }

        public void RemoveCoin()
        {

        }

        public void UpdateWallet(Wallet wallet,User user)
        {
            
            _context.Entry(user).State = EntityState.Detached;
            _context.Wallets.Update(wallet);
            _context.SaveChanges();
           
        }

        public void Detach(Wallet entity)
        {
           
          
        }

        public int CoinsLimit(int userId)
        {
            var coinsLimit = _context.Wallets.FirstOrDefault(x => x.UserId == userId);
            if(coinsLimit != null)
            {
                return coinsLimit.MaxCoins;
            }
            return -1;
        }
    }
}
