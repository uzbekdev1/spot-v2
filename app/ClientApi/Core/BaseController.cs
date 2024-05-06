using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ClientApi.Core
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {

        protected int UserId => int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : 0;

        protected string GetIPAddress()
        {
            var header = Request.Headers["CF-Connecting-IP"].FirstOrDefault() ?? Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (IPAddress.TryParse(header, out var ip))
            {
                return ip.ToString();
            }

            return HttpContext.Connection.RemoteIpAddress.ToString();
        }

        [ProducesDefaultResponseType(typeof(ApiResponse))]
        protected new IActionResult Ok(object result = null)
        {
            var model = new ApiResponse()
            {
                Data = result,
                Error = null,
                Success = true,
            };

            return base.Ok(model);
        }

        [ProducesDefaultResponseType(typeof(ApiResponse))]
        protected IActionResult BadRequest(string error = null)
        {
            var model = new ApiResponse()
            {
                Data = null,
                Error = error,
                Success = false,
            };

            return base.Ok(model);
        }

    }
}