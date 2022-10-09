using CryptoSimulator.DataAccess.Repositories.Interfaces;
using CryptoSimulator.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoSimulator.Services
{
    public class CoinService : ICoinService
    {
        private readonly ICoinRepository _coinRepository;

        public CoinService(ICoinRepository coinRepository)
        {
            _coinRepository = coinRepository;
        }

        public  double GetPriceByCoinId(string coinId)
        {
            var url = $"https://api.coingecko.com/api/v3/simple/price?ids={coinId}&vs_currencies=usd";
            HttpClient client = new HttpClient();
            var raw = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject(raw) as JObject;
            var price = data[coinId]["usd"] as JValue;
            var parsedPrice = (double)price.Value;
            return parsedPrice;
        }

        public static Dictionary<string, double> GetPriceByCoinIds(List<string> coinIds)
        {
            var str = string.Join(",", coinIds);
            var url = $"https://api.coingecko.com/api/v3/simple/price?ids={str}&vs_currencies=usd";
            HttpClient client = new HttpClient();
            var raw = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject(raw) as JObject;
            Dictionary<string, double> coinPrices = new Dictionary<string, double>();
            foreach (var item in data)
            {
                var coin = item.Key;
                var price = item.Value["usd"] as JValue;
                coinPrices.Add(coin, (double)price);
            };
            return coinPrices;
        }
    }
}
