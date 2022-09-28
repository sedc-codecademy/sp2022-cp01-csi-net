using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CryptoSimulatorApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        public int UserId
        {
            get
            {
                return GetAuthorizedUserId();
            }
        }

        private int GetAuthorizedUserId()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?
                .Value, out var userId))
            {
                throw new Exception("Name identifier claim does not exist!");
            }
            return userId;
        }
    }
}
