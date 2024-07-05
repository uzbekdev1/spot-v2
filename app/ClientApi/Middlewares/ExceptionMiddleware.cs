using ClientApi.Core;
using System.Security.Claims;

namespace ClientApi.Middlewares
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        private void WriteLog(string userId, Exception exception)
        {
            _logger.LogWarning($"User#{userId}: Exception - {exception.Message}; StackTrace - {exception.StackTrace}; InnerException - {exception.InnerException}");
        }

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BadHttpRequestException exception)
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

                WriteLog(userId, exception);

                await context.Response.WriteAsync(error.ToString());
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

                WriteLog(userId, exception);

                await context.Response.WriteAsync(error.ToString());
            }
        }

    }

}
