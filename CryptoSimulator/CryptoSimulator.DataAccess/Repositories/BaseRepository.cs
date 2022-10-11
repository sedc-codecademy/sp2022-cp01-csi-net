using CryptoSimulator.DataAccess.Data;

namespace CryptoSimulator.DataAccess.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly CryptoSimulatorDbContext _context;
        public BaseRepository(CryptoSimulatorDbContext context)
        {
            _context = context;
        }
    }
}
