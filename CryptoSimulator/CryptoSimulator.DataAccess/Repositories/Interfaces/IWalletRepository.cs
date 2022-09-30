using CryptoSimulator.DataModels.Models;

namespace CryptoSimulator.DataAccess.Repositories.Interfaces
{
    public interface IWalletRepository
    {
        Wallet GetById(int id);
    }
}
