using CryptoSimulator.ServiceModels.UserModels;
using CryptoSimulator.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoSimulatorApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public ActionResult RegisterUser([FromBody] RegisterUser request)
        {
            try
            {
                _userService.Register(request);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public ActionResult LoginUser([FromBody] LoginModel request)
        {
            try
            {
                var user = _userService.Login(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
