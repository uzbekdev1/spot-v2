using ClientApi.Core;
using Serilog;
using System.Security.Claims;

namespace ClientApi.Middlewares
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                if (context.Response.HasStarted)
                {
                    throw;
                }

                var error = new ApiResponse
                {
                    Data = null,
                    Error = exception.Message,
                    Success = false,
                };

                context.Response.StatusCode = StatusCodes.Status200OK;
                context.Response.ContentType = "application/json";

                var userId = context.Request.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                Log.Error($"User#{userId}: Exception - {exception.Message},");

                await context.Response.WriteAsync(error.ToString());
            }
        }

    }

}
