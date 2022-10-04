using AutoMapper;
using CryptoSimulator.DataAccess.Repositories.Interfaces;
using CryptoSimulator.DataModels.Models;
using CryptoSimulator.ServiceModels.WalletModels;
using CryptoSimulator.Services.Interfaces;
using Newtonsoft.Json;

namespace CryptoSimulator.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;

        public WalletService(IUserRepository userRepository, IWalletRepository walletRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _mapper = mapper;
        }

        public WalletDto GetByUserId(int userId)
        {
            var wallet = _walletRepository.GetByUserId(userId);

            return _mapper.Map<WalletDto>(wallet);
        }

        public double SellCoin(BuySellCoinModel model)
        {
            // TODO: consider updating coin data here, and maybe add notification is changes
            var wallet = GetByUserId(model.UserId);
            var coin = wallet.Coins.FirstOrDefault(x => x.CoinId == model.CoinId && x.WalletId == wallet.Id);
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
                coin.Quantity -= model.Amount;
                wallet.Cash += transaction.TotalPrice;
                user.Transactions.Add(transaction);
                return CalculateYield(model);
            }
            return 0;
        }

        public double BuyCoin(BuySellCoinModel model)
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
            return 0;
        }
        /// <summary>
        /// Calculates the total yield of the portfolio after a transaction (buy or sell) is made
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="coinId"></param>
        /// <param name="amount">Amount of coins to buy. Should be negative value if buying (user loses cash when buying), positive if selling (user gains cash when selling)</param>
        /// <returns></returns>
        // https://api.coingecko.com/api/v3/simple/price?ids={comma-separated coins list}&vs_currencies=usd //api for getting the current price of the coins in the wallet

        public double CalculateYield(BuySellCoinModel model)
        {
            List<double> yields = new List<double>();
            var user = _userRepository.GetById(model.UserId);
            var coins = user.Wallet.Coins;
            //The logic for getting coins data should be added in another service (coin service)
            HttpClient client = new HttpClient();
            var raw = client.GetAsync($"https://api.coingecko.com/api/v3/simple/price?ids={model.CoinId}&vs_currencies=usd").Result.Content.ReadAsStringAsync().Result;
            var update = JsonConvert.DeserializeObject<dynamic>(raw);
            double currentPrice = double.Parse(update[model.CoinId]["usd"]);
            // separate logic for current coin transaction
            var currentCoinYield = model.Amount * currentPrice;
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

        public double AddCash(int userId, double amount)
        {
            var wallet = GetByUserId(userId);
            wallet.Cash += amount;
            // update DB  before return
            return wallet.Cash;
        }

        // TODO ADD NEW MIGRAITON  ===> CHANGED MAXCOIN TO INT
        public void SetMaxCoinLimit(int userId, int limit)
        {
            var user = _userRepository.GetById(userId);
            //All the validations (negative number etc. are done on frontend)
            user.Wallet.MaxCoins = limit;
            // update DB 
        }

        public bool IsCoinLimitReached(int walletId)
        {
            var wallet = _walletRepository.GetById(walletId);
            var coinsCount = wallet.Coins.DistinctBy(c => c.CoinId).ToList().Count;
            return coinsCount == wallet.MaxCoins;
            //var coins = _walletRepository.GetAllCoins(walletId);
        }

        
    }
}
