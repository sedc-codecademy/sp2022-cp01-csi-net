using CryptoSimulator.ServiceModels.UserModels;

namespace CryptoSimulator.Services.Interfaces
{
    public interface IUserService
    {
        void Register(RegisterUser request);
        UserLoginDto Login(LoginModel request);
    }
}
