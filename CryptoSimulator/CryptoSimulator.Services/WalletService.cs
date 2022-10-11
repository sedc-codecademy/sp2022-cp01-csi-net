using AutoMapper;
using CryptoSimulator.DataAccess.Repositories.Interfaces;
using CryptoSimulator.DataModels.Models;
using CryptoSimulator.ServiceModels.WalletModels;
using CryptoSimulator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace CryptoSimulator.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICoinRepository _coinRepository;
        private readonly ICoinService _coinService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public WalletService(IUserRepository userRepository, IWalletRepository walletRepository, IMapper mapper, ILogger<WalletService> logger, ICoinService coinService, ICoinRepository coinRepository, ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _coinRepository = coinRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _logger = logger;
            _coinService = coinService;
        }
        public WalletService()
        {

        }

        public WalletDto GetByUserId(int userId)
        {
            var wallet = _walletRepository.GetByUserId(userId);
            _mapper.Map<Wallet>(wallet);
            return _mapper.Map<WalletDto>(wallet);
        }

        public double GetUserCash(int userId)
        {
            var wallet = GetByUserId(userId);
            return wallet.Cash;
        }

        public double SellCoin(BuySellCoinModel model)
        {
            try
            {

                var wallet = GetByUserId(model.UserId);
                var user = _userRepository.GetById(model.UserId);
                var coin = _coinRepository.GetCoin(wallet.Id, model.Name);
                var coinsWithSameName = _coinRepository.GetAllCoinsInWallet(wallet.Id, model.Name);
                var amountOfCoinsWithSameName = AmountOfCoinsWithSameNameInWallet(coinsWithSameName);
                var priceBoughtOfCoins = PriceBoughtOfCoins(coinsWithSameName);
                var coinPrice = _coinService.GetPriceByCoinId(model.CoinId);
                if (coin != null)
                {
                    if (model.Amount > amountOfCoinsWithSameName)
                    {
                        return 0;
                    }
                    else if (model.Amount == amountOfCoinsWithSameName)
                    {
                        var transaction = new Transaction
                        {
                            BuyOrSell = false,
                            TotalPrice = model.Amount * coinPrice,
                            CoinName = coin.Name,
                            DateCreated = DateTime.Now,
                            Price = coinPrice,
                            Quantity = model.Amount,
                            UserId = model.UserId,
                            User = user
                        };
                        _transactionRepository.Insert(transaction);
                        DeleteAllCoinsFromUser(coinsWithSameName);
                        wallet.Cash += transaction.TotalPrice;
                        wallet.MaxCoins += transaction.Quantity;
                        var convertWallet = _mapper.Map<Wallet>(wallet);
                        _walletRepository.UpdateWallet(convertWallet, user);
                        return CalculateYield(model);

                    }
                    else if (model.Amount < amountOfCoinsWithSameName)
                    {

                        var transaction = new Transaction
                        {
                            BuyOrSell = false,
                            TotalPrice = model.Amount * coinPrice,
                            CoinName = coin.Name,
                            DateCreated = DateTime.Now,
                            Price = coinPrice,
                            Quantity = model.Amount,
                            UserId = model.UserId,
                            User = user
                        };
                        _transactionRepository.Insert(transaction);
                        DeleteAllNeededCoins(coinsWithSameName, model.Amount);
                        wallet.Cash += transaction.TotalPrice;
                        wallet.MaxCoins += transaction.Quantity;
                        var convertWallet = _mapper.Map<Wallet>(wallet);
                        _walletRepository.UpdateWallet(convertWallet, user);
                        user.Transactions.Add(transaction);
                        return CalculateYield(model);
                    }

                }
                return 0;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public double BuyCoin(BuySellCoinModel model)
        {
            var user = _userRepository.GetById(model.UserId);
            var wallet = GetByUserId(model.UserId);
            // Get the current price of the coin from coingecko api
            var coinPrice = _coinService.GetPriceByCoinId(model.CoinId);
            var coin = new Coin
            {
                CoinId = model.CoinId,
                Name = model.Name,
                PriceBought = coinPrice,
                Quantity = model.Amount,
                WalletId = wallet.Id,
            };

            var transaction = new Transaction
            {
                BuyOrSell = true,
                TotalPrice = model.Amount * coinPrice,
                CoinName = coin.Name,
                DateCreated = DateTime.Now,
                Price = coinPrice,
                Quantity = model.Amount,
                UserId = user.Id,
                User = user
            };

            var allCoins = _coinRepository.GetAllCoins(wallet.Id);
            var differentCoinsCount = allCoins.DistinctBy(c => c.CoinId).ToList().Count;
            bool isCoinMaxPassed = differentCoinsCount >= wallet.MaxCoins;
            bool isCoinNewInWallet = allCoins.Any(c => c.CoinId == coin.CoinId);

            if (isCoinMaxPassed && !isCoinNewInWallet)
            {
                throw new Exception("Max coins limit reached!");
            }

            if (transaction.TotalPrice > wallet.Cash)
            {
                throw new Exception("Insuficiend funds!");
            }

            _coinRepository.Insert(coin);
            _transactionRepository.Insert(transaction);
            wallet.Cash -= transaction.TotalPrice;
            var convertWallet = _mapper.Map<Wallet>(wallet);
            _walletRepository.UpdateWallet(convertWallet, user);
            // This value should show up in the top right corner of the wallet each time a coin is bought or sold
            return CalculateYield(model);
        }

        public double AddCash(int userId, double amount)
        {
            try
            {
                if (amount > 0)
                {
                    var wallet = GetByUserId(userId);
                    var walletUser = _userRepository.GetById(wallet.UserId);

                    wallet.Cash += amount;
                    var convertWallet = _mapper.Map<Wallet>(wallet);

                    _walletRepository.UpdateWallet(convertWallet, walletUser);
                    return wallet.Cash;
                }
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                throw new Exception(ex.Message);
            }
        }

        // TODO ADD NEW MIGRAITON  ===> CHANGED MAXCOIN TO INT
        public bool SetMaxCoinLimit(int userId, int limit)
        {
            try
            {
                var wallet = _walletRepository.GetById(userId);
                var walletUser = _userRepository.GetById(wallet.UserId);

                if (limit >= 0 && limit >= wallet.Coins.Count)
                {
                    wallet.MaxCoins = limit;
                    var convertWallet = _mapper.Map<Wallet>(wallet);
                    _walletRepository.UpdateWallet(convertWallet, walletUser);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsCoinLimitReached(int userId)
        {
            try
            {
                var coinLimit = _walletRepository.CoinsLimit(userId);
                if (coinLimit >= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region private 

        /// <summary>
        /// Calculates the total yield of the portfolio after a transaction (buy or sell) is made
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="coinId"></param>
        /// <param name="amount">Amount of coins to buy. Should be negative value if buying (user loses cash when buying), positive if selling (user gains cash when selling)</param>
        /// <returns></returns>
        public double CalculateYield(BuySellCoinModel model)
        {
            List<double> yields = new List<double>();
            var user = _userRepository.GetById(model.UserId);
            var userWallet = _walletRepository.GetByUserId(user.Id);


            var coins = _coinRepository.GetAllCoinsInWallet(userWallet.Id, model.Name);

            if (user != null && userWallet.UserId != 0 && coins.Count > 0)
            {
                var coinPrice = _coinService.GetPriceByCoinId(model.CoinId);
                var currentCoinYield = model.Amount * coinPrice;
                yields.Add(currentCoinYield);

                foreach (var coin in coins)
                {
                    var yield = 0.0;
                    var allUserTransactions = _transactionRepository.GetAllUserTransactionsCoinName(user.Id, coin.Name);

                    foreach (var transaction in allUserTransactions)
                    {
                        yield += transaction.BuyOrSell ? -transaction.TotalPrice : transaction.TotalPrice;
                    }
                    yields.Add(yield);
                }
                var result = yields.Sum();
                return result;
            }
            return 0;
        }

        private double AmountOfCoinsWithSameNameInWallet(List<Coin> listOfCoins)
        {
            double amoutOfCoins = 0;

            foreach (var coin in listOfCoins)
            {
                amoutOfCoins += coin.Quantity;
            }
            return amoutOfCoins;
        }

        private double PriceBoughtOfCoins(List<Coin> listOfCoins)
        {
            double priceBought = 0;

            foreach (var coin in listOfCoins)
            {
                priceBought += coin.PriceBought;
            }
            return priceBought / listOfCoins.Count();
        }

        private void DeleteAllCoinsFromUser(List<Coin> listOfCoins)
        {
            foreach (var coin in listOfCoins)
            {
                _coinRepository.DeleteCoin(coin);
            }
        }

        private void DeleteAllNeededCoins(List<Coin> listOfcoins, double amoutOfCoins)
        {
            var i = 0;
            while (amoutOfCoins > 0)
            {
                if (amoutOfCoins >= listOfcoins[i].Quantity)
                {
                    _coinRepository.DeleteCoin(listOfcoins[i]);
                    amoutOfCoins -= listOfcoins[i].Quantity;
                }
                else
                {
                    listOfcoins[i].Quantity -= amoutOfCoins;
                    _coinRepository.UpdateCoin(listOfcoins[i]);
                    amoutOfCoins = 0;
                }
                i++;
            }
        }



        #endregion
    }
}
