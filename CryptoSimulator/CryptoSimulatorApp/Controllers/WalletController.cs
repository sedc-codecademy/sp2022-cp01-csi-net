using CryptoSimulator.ServiceModels.WalletModels;
using CryptoSimulator.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

       
        [HttpPost]
        //[AllowAnonymous]
        [Route("SellCoin")]
        public bool SellCoin(BuySellCoinModel model)
        {
            try
            {
                var transaction = _walletService.SellCoin(model);
                if (transaction != 0)
                {

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        [HttpGet("transactions")]
        public IActionResult GetUserTransactions(int userId)
        {
            try
            {
                var transactions = _walletService.GetUsersTransactions(userId);
                return Ok(transactions);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
