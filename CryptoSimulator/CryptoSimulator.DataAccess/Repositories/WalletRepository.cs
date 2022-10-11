using CryptoSimulator.DataAccess.Data;
using CryptoSimulator.DataAccess.Repositories.Interfaces;
using CryptoSimulator.DataModels.Models;

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
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            var wallet = new Wallet();
            if (user != null)
            {
                wallet = user.Wallet;
            }
            return wallet ;
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


    }
}
