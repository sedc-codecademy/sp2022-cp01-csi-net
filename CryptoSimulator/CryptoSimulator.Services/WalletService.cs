using AutoMapper;
using CryptoSimulator.DataAccess.Repositories.Interfaces;
using CryptoSimulator.DataModels.Models;
using CryptoSimulator.ServiceModels.WalletModels;
using CryptoSimulator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSimulator.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUserService _userService;
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;

        public WalletService(IUserService userService, IWalletRepository walletRepository, IMapper mapper)
        {
            _userService = userService;
            _walletRepository = walletRepository;
            _mapper = mapper;
        }

        public WalletDto GetByUserId(int userId)
        {
            var wallet = _walletRepository.GetById(userId);

            return _mapper.Map<WalletDto>(wallet);
        }

        public double SellCoins(string coinId, int userId, double amount)
        {
            // TODO: consider updating coin data here, and maybe add notification is changes
            var wallet = GetByUserId(userId);
            var coin = wallet.Coins.FirstOrDefault(x => x.CoinId == coinId && x.WalletId == wallet.Id);
            if (coin != null)
            {
                var user = _userService.GetById(userId);
                var transaction = new Transaction
                {
                    BuyOrSell = false,
                    TotalPrice = amount * coin.PriceBought,
                    CoinName = coin.Name,
                    DateCreated = DateTime.Now,
                    Price = coin.PriceBought,
                    Quantity = amount,
                    UserId = userId,
                    User = user
                };
                coin.Quantity -= amount;
                wallet.Cash += transaction.TotalPrice;
                user.Transactions.Add(transaction);
                return wallet.Cash;
            }
            return 0;
        }

        public double BuyCoins(string coinId, int userId, double amount)
        {
            // TODO: consider updating coin data here, and maybe add notification if changes
            var wallet = GetByUserId(userId);
            var coin = wallet.Coins.FirstOrDefault(x => x.CoinId == coinId && x.WalletId == wallet.Id);
            if (coin != null)
            {
                var user = _userService.GetById(userId);
                var transaction = new Transaction
                {
                    BuyOrSell = true,
                    TotalPrice = amount * coin.PriceBought,
                    CoinName = coin.Name,
                    DateCreated = DateTime.Now,
                    Price = coin.PriceBought,
                    Quantity = amount,
                    UserId = userId,
                    User = user
                };
                coin.Quantity += amount;
                wallet.Cash -= transaction.TotalPrice;
                user.Transactions.Add(transaction);
                return wallet.Cash;
            }
            return 0;
        }
        /// <summary>
        /// Calculated the yield a user is getting if he proceeds with the purchase/sell
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="coinId"></param>
        /// <param name="amount">Amount of coins to buy. Should be negative value if buying (user loses cash when buying), positive if selling (user gains cash when selling)</param>
        /// <returns></returns>
        /// 

        // https://api.coingecko.com/api/v3/simple/price?{comma-separated coins list}&vs_currencies=usd //api for getting the current price of the coins in the wallet
        public double CalculateYield(int userId, int coinId, double amount)
        {
            List<double> yields = new List<double>();
            var user = _userService.GetById(userId);
            var coins = user.Wallet.Coins;
            var coinIDs = coins.Select(x => x.CoinId);
            var coinString = "ids=" + string.Join(",", coinIDs); // use this to update coin values
            foreach (var coin in coins)
            {
                var yield = 0.0;
                var transactions = user.Transactions.Where(x => x.CoinName == coin.Name);
                foreach (var transaction in transactions)
                {
                    yield += transaction.BuyOrSell ? -transaction.TotalPrice : transaction.TotalPrice;
                }
                if (coinId == coin.Id)
                {
                    yield += amount * coin.PriceBought;
                }
                yields.Add(yield);
            }
            var result = yields.Sum();
            return result;
        }

        public double AddCash(int userId, double amount)
        {
            var wallet = GetByUserId(userId);
            wallet.Cash += amount;
            // update DB  before return
            return wallet.Cash;
        }

        public void SetMaxCoinLimit(int userId, double limit)
        {
            var user = _userService.GetById(userId);
            //All the validations (negative number etc. are done on frontend)
            user.Wallet.MaxCoins = limit;
            // update DB 
        }
    }
}
