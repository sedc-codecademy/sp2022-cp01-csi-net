using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoSimulator.Services
{
    public class CoinService
    {
        public static double GetPriceByCoinId(string coinId)
        {
            var url = $"https://api.coingecko.com/api/v3/simple/price?ids={coinId}&vs_currencies=usd";
            HttpClient client = new HttpClient();
            var raw = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject(raw) as JObject;
            var price = data[coinId]["usd"] as JValue;
            var parsedPrice = (double)price.Value;
            return parsedPrice;
        }
    }
}
