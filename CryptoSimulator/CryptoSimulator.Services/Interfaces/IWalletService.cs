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
        public double SellCoin(BuySellCoinModel model);
        public double BuyCoin(BuySellCoinModel model);
        public double CalculateYield(BuySellCoinModel model);
        public double AddCash(int userId, double amount);
        public void SetMaxCoinLimit(int userId, double limit);
    }
}
