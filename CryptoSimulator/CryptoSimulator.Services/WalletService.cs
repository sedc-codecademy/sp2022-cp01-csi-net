using AutoMapper;
using CryptoSimulator.DataAccess.Repositories.Interfaces;
using CryptoSimulator.DataModels.Models;
using CryptoSimulator.ServiceModels.WalletModels;
using CryptoSimulator.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CryptoSimulator.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ICoinService _coinService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public WalletService(IUserRepository userRepository, IWalletRepository walletRepository, IMapper mapper, ILogger<WalletService> logger, ICoinService coinService)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
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

                var user = _userRepository.GetById(model.UserId);
                var coin = _coinRepository.GetCoin(wallet.Id,model.Name);
                var coinsWithSameName = _coinRepository.GetAllCoinsInWallet(wallet.Id, model.Name);
                var amountOfCoinsWithSameName = AmountOfCoinsWithSameNameInWallet(coinsWithSameName);
                var priceBoughtOfCoins = PriceBoughtOfCoins(coinsWithSameName);
                var coinPrice = _coinService.GetPriceByCoinId(model.CoinId);
                     if(coin != null) {
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
                var wallet = GetByUserId(model.UserId);
                var coin = wallet.Coins.FirstOrDefault(x => x.CoinId == model.CoinId && x.WalletId == wallet.Id);
                // Get the current price of the coin
                //let currentCoinPrice = get coin price from coingecko api
                if (coin != null)
                {
                    var user = _userRepository.GetById(model.UserId);
                    var transaction = new Transaction
                    {
                        BuyOrSell = true,
                        TotalPrice = model.Amount * coin.PriceBought, //it should be model.Amount * currentCoinPrice
                        CoinName = coin.Name,
                        DateCreated = DateTime.Now,
                        Price = coin.PriceBought, //it should be currentCoinPrice
                        Quantity = model.Amount,
                        UserId = user.Id,
                        User = user
                    };
                    coin.Quantity += model.Amount;
                    wallet.Cash -= transaction.TotalPrice;
                    user.Transactions.Add(transaction);
                    return CalculateYield(model);
                }
                var newCoin = new Coin
                {
                    CoinId = model.CoinId,
                    Name = model.Name,
                    PriceBought = 50,
                    Quantity = model.Amount,
                    WalletId = wallet.Id,
                    }

                var userCoin = _userRepository.GetById(model.UserId);
                var transaction2 = new Transaction
                {
                    BuyOrSell = true,
                    TotalPrice = model.Amount * newCoin.PriceBought, //it should be model.Amount * currentCoinPrice
                    CoinName = newCoin.Name,
                    DateCreated = DateTime.Now,
                    Price = newCoin.PriceBought, //it should be currentCoinPrice
                    Quantity = model.Amount,
                    UserId = userCoin.Id,
                    User = userCoin
                };
                 
                newCoin.Quantity += model.Amount;
                wallet.Cash -= transaction2.TotalPrice;
                userCoin.Transactions.Add(transaction2);
                return CalculateYield(model);
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
               
                wallet.Cash += amount;
                // update DB  before return
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
                var user = _userRepository.GetById(userId);
                //All the validations (negative number etc. are done on frontend)
                user.Wallet.MaxCoins = limit;
                // update DB 
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public bool IsCoinLimitReached(int walletId)
        {
            try
            {
                var wallet = _walletRepository.GetById(walletId);
                
                var coinsCount = wallet?.Coins?.DistinctBy(c => c.CoinId).ToList().Count;
                if(wallet != null)
                {
                    return coinsCount == wallet.MaxCoins;
                }

                return false;
                //var coins = _walletRepository.GetAllCoins(walletId);
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
            var coins = user.Wallet.Coins;
            //The logic for getting coins data should be added in another service (coin service)
            var coinPrice = _coinService.GetPriceByCoinId(model.CoinId);
            // separate logic for current coin transaction
            var currentCoinYield = model.Amount * coinPrice;
            yields.Add(currentCoinYield);
            foreach (var coin in coins)
            {
                var yield = 0.0;
                var transactions = user.Transactions.Where(x => x.CoinName == coin.Name);
                foreach (var transaction in transactions)
                {
                    yield += transaction.BuyOrSell ? -transaction.TotalPrice : transaction.TotalPrice;
                }
                yields.Add(yield);
            }
            var result = yields.Sum();
            return result;
        }

        double IWalletService.CalculateYield(BuySellCoinModel model)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
