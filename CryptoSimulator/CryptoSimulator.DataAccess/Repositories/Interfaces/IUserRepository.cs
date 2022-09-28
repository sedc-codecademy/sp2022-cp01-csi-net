using CryptoSimulator.DataModels.Models;

namespace CryptoSimulator.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        int Insert(User entity);
    }
}
