using CryptoSimulator.ServiceModels.WalletModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSimulator.Services.Interfaces
{
    public interface IWalletService
    {
        public WalletDto GetByUserId(int userId);
        public double SellCoins(string coinId, int userId, double amount);
        public double BuyCoins(string coinId, int userId, double amount);
        public double CalculateYield(int userId, int coinId, double amount);
        public double AddCash(int userId, double amount);
        public void SetMaxCoinLimit(int userId, double limit);
    }
}
