using CryptoSimulator.DataAccess.Data;

namespace CryptoSimulator.DataAccess.Repositories
{
    public abstract class BaseRepository
    {
        private readonly CryptoSimulatorDbContext _context;
        public BaseRepository(CryptoSimulatorDbContext context)
        {
            _context = context;
        }
    }
}
