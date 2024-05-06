using Microsoft.AspNetCore.Mvc;
using TraceApi.Models;

namespace TraceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MonitoringController : ControllerBase
    {
        private readonly ILogger<MonitoringController> _logger;

        public MonitoringController(ILogger<MonitoringController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(IEnumerable<AuditLogModel>))]
        public IActionResult GetLogs()
        {
            return Ok();
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(int))]
        public IActionResult AddLog([FromBody] AuditLogModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(IEnumerable<AppErrorModel>))]
        public IActionResult GetErrors()
        {
            return Ok();
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(int))]
        public IActionResult AddError([FromBody] AppErrorModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }


    }
}
