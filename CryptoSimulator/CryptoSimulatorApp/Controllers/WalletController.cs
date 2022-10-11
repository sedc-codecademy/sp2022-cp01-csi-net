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

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("User")]
        //public IActionResult GetUserById(int id)
        //{
        //    var user = _walletService.GetByUserId(id);
        //    if(user.Cash == 0)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(user);
        //}

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

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddCash")]
        public IActionResult AddCash(int userId, double amount)
        {
            try
            {
                var addCash = _walletService.AddCash(userId, amount);
                if (addCash != 0)
                {
                    return Ok(addCash);
                    // huh?
                }

                return Ok(addCash);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("SetCashToMax")]
        public void SetCashToMax(int userId, int limit)
        {
            try
            {
                _walletService.SetMaxCoinLimit(userId, limit);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
