using AutoMapper;
using CryptoSimulator.DataModels.Models;
using CryptoSimulator.ServiceModels.UserModels;
using CryptoSimulator.ServiceModels.WalletModels;

namespace CryptoSimulator.Common.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<RegisterUser, User>();
          
        }
    }
}
