using CryptoSimulator.ServiceModels.WalletModels;
using CryptoSimulator.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CryptoSimulatorApp.Controllers
{
    public class WalletController : BaseController
    {
        IWalletService _walletService;
        ILogger _logger;
        public WalletController(IWalletService walletService,ILogger<WalletController> logger)
        {
            _walletService = walletService;
            _logger = logger;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("User")]
        public IActionResult GetUserById(int id)
        {
            var user = _walletService.GetByUserId(id);
            if(user.Cash == 0)
            {
                return NotFound();
            }
            return Ok(user);

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("SellCoin")]
        public bool SellCoin(BuySellCoinModel model)
        {
            
            try
            {
                var transaction = _walletService.SellCoin(model);
                if(transaction != 0)
                {
                   
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("BuyCoin")]
        public bool BuyCoin (BuySellCoinModel model)
        {
            try
            {
                var buyTransaction = _walletService.BuyCoin(model);
                if(buyTransaction != 0)
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
                if(addCash != 0)
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

        [HttpGet]
        [AllowAnonymous]
        [Route("IsLimitReached")]
        public IActionResult IsCoinLimitReached(int walletId)
        {
            try
            {
                var isCoinLimitReached = _walletService.IsCoinLimitReached(walletId);
                return Ok(isCoinLimitReached);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
