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
            try
            {
                var ip = HttpContext?.Connection?.RemoteIpAddress?.ToString();
                if (!string.IsNullOrEmpty(ip))
                    return ip;

                return HttpContext.Request.Headers.ContainsKey("X-Forwarded-For") ? IPAddress.Parse(HttpContext.Request.Headers["X-Forwarded-For"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries)[0]).ToString() : HttpContext?.Connection?.RemoteIpAddress?.ToString();
            }
            catch
            {
                return "0.0.0.0";
            }
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