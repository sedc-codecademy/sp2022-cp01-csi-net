using AutoMapper;
using CryptoSimulator.DataModels.Models;
using CryptoSimulator.ServiceModels.WalletModels;

namespace CryptoSimulator.Common.Mappers
{
    public class WalletMapper : Profile
    {
        public WalletMapper()
        {
            CreateMap<WalletDto, Wallet>();
        }
    }
}
