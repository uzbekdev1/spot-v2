using ClientApi.Core;
using ClientApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ClientApi.Controllers
{
    public class CommonController : BaseController
    {
        private readonly ILogger<CommonController> _logger;

        private readonly IConfiguration _configuration;

        public CommonController(IConfiguration configuration, ILogger<CommonController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<string>))]
        public IActionResult CheckVersion()
        {
            try
            {
                var version = _configuration["AppVersion"];

                return Ok(version);
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { }));
                return BadRequest(exp.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DownloadClient([FromQuery] bool test)
        {
            var clientIp = "";
            var version = "";
            try
            {
                clientIp = GetIPAddress();
                version = _configuration["AppVersion"];
                var root = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Program)).Location);
                var path = Path.Combine(root, "Files", "SpotClient", test ? "Debug" : "Release", "SpotLauncher.exe");
                var bytes = await System.IO.File.ReadAllBytesAsync(path);

                _logger.LogInformation($"Client {clientIp}: Download app v{version}");

                return File(bytes, "application/octet-stream", "SpotLauncher.exe");
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { test, clientIp, version }));
                return BadRequest(exp.Message);
            }
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<string>))]
        public IActionResult CheckConnection()
        {
            return Ok($"{DateTime.Now:dd-MM-yyyy HH:mm:ss.fff}");
        }
    }
}
