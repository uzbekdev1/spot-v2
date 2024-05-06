using Microsoft.AspNetCore.Mvc;

namespace TimerBroadcast.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TimeController : ControllerBase
    {
        [HttpGet]
        [ProducesDefaultResponseType(typeof(double))]
        public IActionResult Now()
        {
            var timeNow = DateTime.Now;
            var milliSeconds = timeNow.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

            Console.WriteLine($"Time now: {timeNow} ({milliSeconds})");

            return Ok(milliSeconds);
        }
    }
}
