using AutoMapper;
using CryptoSimulator.DataAccess.Repositories.Interfaces;
using CryptoSimulator.DataModels.Models;
using CryptoSimulator.ServiceModels.WalletModels;
using CryptoSimulator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
        public WalletService(IUserRepository userRepository, IWalletRepository walletRepository, IMapper mapper, ILogger<WalletService> logger, ICoinService coinService,ICoinRepository coinRepository,ITransactionRepository transactionRepository)
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

        public double SellCoin(BuySellCoinModel model)
        {
            // TODO: consider updating coin data here, and maybe add notification is changes
            try
            {
                var wallet = GetByUserId(model.UserId);
                var coin = _coinRepository.GetById(wallet.Id);
                if (coin != null)
                {
                    var user = _userRepository.GetById(model.UserId);
                    var transaction = new Transaction
                    {
                        BuyOrSell = false,
                        TotalPrice = model.Amount * coin.PriceBought,
                        CoinName = coin.Name,
                        DateCreated = DateTime.Now,
                        Price = coin.PriceBought,
                        Quantity = model.Amount,
                        UserId = model.UserId,
                        User = user
                    };
                    _transactionRepository.Insert(transaction);
                    coin.Quantity -= model.Amount;
                    wallet.Cash += transaction.TotalPrice;
                    user.Transactions.Add(transaction);
                    return CalculateYield(model);
                }
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                throw new Exception(ex.Message);
            }
        }

        public double BuyCoin(BuySellCoinModel model)
        {
            try
            {
                // TODO: consider updating coin data here, and maybe add notification if changes
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
                wallet.Coins.Add(coin);
                

                var userCoin = _userRepository.GetById(model.UserId);
                var transaction = new Transaction
                {
                    BuyOrSell = true,
                    TotalPrice = (model.Amount) * coinPrice,
                    CoinName = coin.Name,
                    DateCreated = DateTime.Now,
                    Price = coinPrice,
                    Quantity = model.Amount,
                    UserId = userCoin.Id,
                    User = userCoin
                };
                
 
                var isCoinMaxPassed = IsCoinLimitReached(wallet.Id);

                if (!isCoinMaxPassed && wallet.Cash >= transaction.TotalPrice && wallet.UserId != null)
                {
                    _coinRepository.Insert(coin);
                    _transactionRepository.Insert(transaction);
                    wallet.Cash -= transaction.TotalPrice;
                    wallet.MaxCoins -= coin.Quantity;
                    var convertWallet = _mapper.Map<Wallet>(wallet);
                    _walletRepository.UpdateWallet(convertWallet, user);
                    return CalculateYield(model);

                }
                          
                    return 0;
               
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                throw new Exception(ex.Message);

            }
        }
        /// <summary>
        /// Calculates the total yield of the portfolio after a transaction (buy or sell) is made
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="coinId"></param>
        /// <param name="amount">Amount of coins to buy. Should be negative value if buying (user loses cash when buying), positive if selling (user gains cash when selling)</param>
        /// <returns></returns>
        // https://api.coingecko.com/api/v3/simple/price?ids={comma-separated coins list}&vs_currencies=usd //api for getting the current price of the coins in the wallet

        

        public double AddCash(int userId, double amount)
        {
            try
            {
                var wallet = GetByUserId(userId);
                var walletUser = _userRepository.GetById(wallet.UserId);
               
                wallet.Cash += amount;
                var convertWallet = _mapper.Map<Wallet>(wallet);
             
                _walletRepository.UpdateWallet(convertWallet,walletUser);
                return wallet.Cash;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                throw new Exception(ex.Message);
            }
        }

        // TODO ADD NEW MIGRAITON  ===> CHANGED MAXCOIN TO INT
        public void SetMaxCoinLimit(int userId, int limit)
        {
            try
            {
                var wallet = _walletRepository.GetById(userId);
                var walletUser = _userRepository.GetById(wallet.UserId);
                wallet.MaxCoins = limit;
                var convertWallet = _mapper.Map<Wallet>(wallet);
                _walletRepository.UpdateWallet(convertWallet, walletUser);
            }
            catch(Exception ex) {
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
            catch(Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                throw new Exception(ex.Message);
            }
        }

        #region private 
        private double CalculateYield(BuySellCoinModel model)
        {
            List<double> yields = new List<double>();

            var user = _userRepository.GetById(model.UserId);

            var userWallet = _walletRepository.GetByUserId(user.Id);

            var coins = _coinRepository.GetAllCoinsInWallet(userWallet.Id);


            if (user != null && userWallet.UserId != null && coins.Count >0 ) {
                var coinPrice = _coinService.GetPriceByCoinId(model.CoinId);
                var currentCoinYield = model.Amount * coinPrice;
                yields.Add(currentCoinYield);

                foreach (var coin in coins)
                {
                    var yield = 0.0;
                    var allUserTransactions = _transactionRepository.GetAllUserTransactions(user.Id);
                    var transactions = allUserTransactions.Where(x => x.CoinName == coin.Name);
                    foreach (var transaction in transactions)
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

        double IWalletService.CalculateYield(BuySellCoinModel model)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
