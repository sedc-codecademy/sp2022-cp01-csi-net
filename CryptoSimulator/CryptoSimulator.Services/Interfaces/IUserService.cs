using CryptoSimulator.DataModels.Models;
using CryptoSimulator.ServiceModels.UserModels;

namespace CryptoSimulator.Services.Interfaces
{
    public interface IUserService
    {
        void Register(RegisterUser request);
        UserLoginDto Login(LoginModel request);
        User GetById(int id);
    }
}
