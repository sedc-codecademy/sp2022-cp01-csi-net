using AutoMapper;
using CryptoSimulator.DataModels.Models;
using CryptoSimulator.ServiceModels.UserModels;

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
