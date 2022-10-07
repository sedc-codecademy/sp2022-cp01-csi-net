using CryptoSimulator.DataModels.Models;

namespace CryptoSimulator.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByUsername(string username);
        int Insert(User entity);
    }
}
