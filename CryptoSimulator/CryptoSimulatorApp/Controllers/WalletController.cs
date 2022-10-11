using CryptoSimulator.DataModels.Models;
using CryptoSimulator.ServiceModels.WalletModels;
using CryptoSimulator.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CryptoSimulatorApp.Controllers
{
    public class WalletController : BaseController
    {
        IWalletService _walletService;
        ILogger _logger;
        public WalletController(IWalletService walletService, ILogger<WalletController> logger)
        {
            _walletService = walletService;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetCoins")]
        public IActionResult GetWalletCoins(int userId)
        {
            try
            {
                var wallet = _walletService.GetByUserId(userId);
                var rsp = new Dictionary<string, List<CoinDto>>();
                rsp.Add("coins", wallet.Coins.Select(x=>new CoinDto
                {
                    CoinId = x.CoinId,
                    Name = x.Name,
                    PriceBought = x.PriceBought,
                    Quantity = x.Quantity,
                    WalletId = x.WalletId,
                }).ToList());
                //var result = JsonConvert.SerializeObject(rsp);
                //var jsoned = JsonConvert.DeserializeObject(result);
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("CalculateYield")]
        public IActionResult CalculateYield(BuySellCoinModel model)
        {
            try
            {
                var result = _walletService.CalculateYield(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("SellCoin")]
        public IActionResult SellCoin(BuySellCoinModel model)
        {
            try
            {
                //model.UserId = UserId;
                var sellTransaction = _walletService.SellCoin(model);
                return Ok(sellTransaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("BuyCoin")]
        public IActionResult BuyCoin(BuySellCoinModel model)
        {
            try
            {
                //model.UserId = UserId;
                var buyTransaction = _walletService.BuyCoin(model);
                return Ok(buyTransaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user-cash")]
        public IActionResult ShowCash(int userId)
        {
            try
            {
                var userCash = _walletService.GetUserCash(userId);
                return Ok(userCash);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("add-cash")]
        public IActionResult AddCash(int userId, double amount)
        {
            try
            {
                var addCash = _walletService.AddCash(userId, amount);
                if (addCash != 0)
                {
                    return Ok(addCash);
                }
                else
                {
                    throw new Exception("Please enter a value");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("set-coin-limit")]
        public IActionResult SetCashToMax(int userId, int limit)
        {
            try
            {
                var result = _walletService.SetMaxCoinLimit(userId,limit);
                return Ok(result);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
