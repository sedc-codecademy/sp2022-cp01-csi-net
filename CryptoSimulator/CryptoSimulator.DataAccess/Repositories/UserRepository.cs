using CryptoSimulator.DataAccess.Data;
using CryptoSimulator.DataAccess.Repositories.Interfaces;
using CryptoSimulator.DataModels.Models;

namespace CryptoSimulator.DataAccess.Repositories
{
    public class UserRepository : BaseRepository, IRepository<User>, IUserRepository
    {
        public UserRepository(CryptoSimulatorDbContext context) : base(context)
        {
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
