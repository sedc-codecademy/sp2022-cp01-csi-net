using CryptoSimulator.ServiceModels.WalletModels;

namespace CryptoSimulator.Services.Interfaces
{
    public interface IWalletService
    {
        public WalletDto GetByUserId(int userId);
        public double SellCoin(BuySellCoinModel model);
        public double BuyCoin(BuySellCoinModel model);
        public double CalculateYield(BuySellCoinModel model);
        public double AddCash(int userId, double amount);
        public bool SetMaxCoinLimit(int userId, int limit);
        public double GetUserCash(int userId);
    }
}
