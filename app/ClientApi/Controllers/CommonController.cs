using ClientApi.Core;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Reflection;

namespace ClientApi.Controllers
{
    public class CommonController : BaseController
    {

        private readonly IConfiguration _configuration;

        public CommonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<string>))]
        public IActionResult CheckVersion()
        {
            var version = _configuration["AppVersion"];

            return Ok(version);
        }
         
        [HttpGet]
        public IActionResult DownloadClient([FromQuery] bool test)
        {
            var version = _configuration["AppVersion"];
            var root = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Program)).Location);
            var path = Path.Combine(root, "Files", "SpotClient", test ? "Debug" : "Release", "SpotLauncher.exe");
            var bytes = System.IO.File.ReadAllBytes(path);

            Log.Information($"Client {GetIPAddress()}: Download app v{version}");

            return File(bytes, "application/octet-stream", "SpotLauncher.exe");
        }

    }
}
