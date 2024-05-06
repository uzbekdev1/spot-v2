using ClientApi.Core;
using ClientApi.Dtos;
using ClientApi.Helpers;
using ClientApi.Models;
using ClientApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClientApi.Controllers
{
    public class AccountController : BaseController
    {

        private readonly SpotService _spotService;

        private readonly CryptographyHelper _cryptographyHelper;

        private readonly IConfiguration _configuration;

        public AccountController(SpotService spotService, CryptographyHelper cryptographyHelper, IConfiguration configuration)
        {
            _spotService = spotService;
            _cryptographyHelper = cryptographyHelper;
            _configuration = configuration;
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(ApiResponse<UserInfo>))]
        public IActionResult Login([FromBody] LoginEmbed model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad data");
            }

            var decode = _cryptographyHelper.Decrypt(model.raw);

            if (string.IsNullOrEmpty(decode))
            {
                return BadRequest("Invalid format");
            }

            var result = JsonConvert.DeserializeObject<LoginModel>(decode);

            if (result == null)
            {
                return BadRequest("Invalid data");
            }

            if (!Guid.TryParse(result.uid, out var uid) || uid == Guid.Empty)
            {
                return BadRequest("Invalid key");
            }

            var info = _spotService.GetUser(result.login, result.password, result.uid);

            if (info == null)
            {
                return BadRequest("User not found");
            }

            Log.Information($"User info: {JsonConvert.SerializeObject(info, Formatting.None)}");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, info.login),
                        new Claim(ClaimTypes.NameIdentifier, $"{info.id}"),
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new UserInfo
            {
                Login = info.login,
                Name = $"{info.first_name} {info.last_name} {info.second_name}",
                Token = tokenHandler.WriteToken(token),
                Windows = info.window_count,
                Version = _configuration["AppVersion"],
                TimeUzExUrl = _configuration["TimeUzExUzUrl"]
            });
        }

    }
}
