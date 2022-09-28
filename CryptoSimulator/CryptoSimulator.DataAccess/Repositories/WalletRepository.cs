using CryptoSimulator.DataAccess.Data;
using CryptoSimulator.DataAccess.Repositories.Interfaces;
using CryptoSimulator.DataModels.Models;

namespace CryptoSimulator.DataAccess.Repositories
{
    public class WalletRepository : BaseRepository, IRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(CryptoSimulatorDbContext context) : base(context)
        {
        }

        public Wallet GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
