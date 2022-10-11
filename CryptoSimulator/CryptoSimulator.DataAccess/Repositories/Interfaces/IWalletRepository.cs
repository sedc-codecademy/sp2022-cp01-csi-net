using CryptoSimulator.DataModels.Models;
using CryptoSimulator.ServiceModels.WalletModels;

namespace CryptoSimulator.DataAccess.Repositories.Interfaces
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        Wallet GetByUserId(int userId);

        public void UpdateWallet(Wallet entity, User user);

        void Detach(Wallet entity);

        public int CoinsLimit(int userId);

        //IEnumerable<Coin> GetAllCoins(int walletId);
    }
}
