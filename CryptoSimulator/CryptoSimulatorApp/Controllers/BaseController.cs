using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoSimulatorApp.Controllers
{
    [Authorize]
    public abstract class BaseController : ControllerBase
    {

    }
}
