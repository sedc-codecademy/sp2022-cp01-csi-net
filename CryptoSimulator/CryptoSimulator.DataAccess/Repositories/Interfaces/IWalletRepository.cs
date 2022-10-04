using CryptoSimulator.DataModels.Models;

namespace CryptoSimulator.DataAccess.Repositories.Interfaces
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        Wallet GetByUserId(int userId);
        //IEnumerable<Coin> GetAllCoins(int walletId);
    }
}
