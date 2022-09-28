using CryptoSimulator.DataModels.Models;

namespace CryptoSimulator.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int id);
    }
}
