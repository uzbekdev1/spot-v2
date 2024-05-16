using ClientApi.Core;
using ClientApi.Dtos;
using ClientApi.Filters;
using ClientApi.Helpers;
using ClientApi.Models;
using ClientApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Newtonsoft.Json;
using Serilog;
using System.Text;

namespace ClientApi.Controllers
{
    [CheckAuth]
    public class CabinetController : BaseController
    {

        private readonly SpotService _spotService;

        private readonly NewSpotService _newSpotService;

        private readonly CryptographyHelper _cryptographyHelper;

        public CabinetController(SpotService spotService, NewSpotService newSpotService, CryptographyHelper cryptographyHelper)
        {
            _spotService = spotService;
            _newSpotService = newSpotService;
            _cryptographyHelper = cryptographyHelper;
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<double>))]
        [EnableRateLimiting(RateLimiterPolicies.fixed_3_limit_in_1_sec)]
        public IActionResult CheckTimeV2()
        {
            var timeNow = DateTime.Now;
            var milliSeconds = timeNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;

            return Ok(milliSeconds);
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<ContactItem>>))]
        public IActionResult GetContracts([FromQuery] string search)
        {
            var items = _spotService.GetContracts(search);

            return Ok(items);
        }

        [HttpGet("{partId}")]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<MainContact>>))]
        public IActionResult MainContracts([FromRoute] int partId, [FromQuery] string search = "", [FromQuery] bool isProd = false)
        {
            var items = _spotService.MainContracts(UserId, partId, search, isProd);

            return Ok(items);
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public IActionResult CreateOrder([FromBody] OrderEmbed model, [FromServices] AmqpService amqpService)
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

            var result = JsonConvert.DeserializeObject<OrderForm>(decode);

            if (result == null)
            {
                return BadRequest("Invalid data");
            }

            if (!Guid.TryParse(result.uid, out var uid) || uid == Guid.Empty)
            {
                return BadRequest("Invalid key");
            }

            var serverDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            try
            {

                amqpService.PushMessage(UserId, result.contractId, result.kolvo, result.inp, result.price, GetIPAddress(), result.clientDate, serverDate, result.uid, result.clientVersion, _spotService.LatestToken(), result.dbDate);

                Log.Information($"Order time: {serverDate}; Send order: {JsonConvert.SerializeObject(result, Formatting.None)}");

            }
            catch (Exception exp)
            {
                Log.Error(exp.Message);
            }

            return Ok();
        }

        [HttpPost("{orderId}")]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public IActionResult DeleteOrder([FromRoute] int orderId)
        {
            _spotService.DeleteOrder(orderId, UserId, GetIPAddress());

            return Ok();
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<MyOrderResult>>))]
        [EnableRateLimiting(RateLimiterPolicies.fixed_3_limit_in_1_sec)]
        public IActionResult MyOrders()
        {
            var results = _spotService.MyOrders(UserId);

            return Ok(results);
        }

        [HttpGet("{contractId}")]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<OrderItem>>))]
        [EnableRateLimiting(RateLimiterPolicies.fixed_3_limit_in_1_sec)]
        public IActionResult GetOrders([FromRoute] int contractId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad data");
            }

            var results = _spotService.GetOrders(contractId, UserId);

            foreach (var item in results)
            {
                item.mine = item.traderid == UserId;
            }

            return Ok(results);
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<PetroClient>>))]
        [EnableRateLimiting(RateLimiterPolicies.fixed_5_limit_in_1_sec)]
        public IActionResult GetClients()
        {
            var results = _spotService.GetClients(UserId);

            return Ok(results);
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<ContractPart>>))]
        public IActionResult GetParts()
        {
            var results = _spotService.GetParts();

            return Ok(results);
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<ContactItem>>))]
        [EnableRateLimiting(RateLimiterPolicies.fixed_5_limit_in_1_sec)]
        public IActionResult GetContractsWithId([FromQuery] string search)
        {
            var items = _spotService.GetContractsWithId(search);

            return Ok(items);
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<PetroClient>>))]
        [EnableRateLimiting(RateLimiterPolicies.fixed_3_limit_in_1_sec)]
        public IActionResult SearchClient([FromQuery] int inp)
        {
            var results = _spotService.SearchClient(UserId, inp);

            return Ok(results);
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<PetroClient>>))]
        public IActionResult SetClient([FromQuery] int inp)
        {
            var results = _spotService.SetClient(UserId, inp);

            return Ok(results);
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<PetroClient>>))]
        public IActionResult RemoveClient([FromQuery] int inp)
        {
            var results = _spotService.RemoveClient(UserId, inp);

            return Ok(results);
        }

        [HttpGet()]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<MainContact>>))]
        public IActionResult NewSpotMainContracts([FromQuery] string search = "")
        {
            var items = _newSpotService.MainContracts(search);
            var list = new List<MainContact>();

            foreach (var item in items)
            {
                list.Add(new MainContact
                {
                    par1 = item.contractId,
                    par2 = $"{item.productName.Trim()} - {item.sellerName.Trim()}",
                    par4 = item.startPrice,
                    newSpotContractNumber = item.number.Trim()
                });
            }

            return Ok(list);
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<Quote>>))]
        [EnableRateLimiting(RateLimiterPolicies.fixed_3_limit_in_1_sec)]
        public IActionResult GetQuotes([FromQuery] int contractId)
        {
            var results = _spotService.GetQuotes(UserId, contractId);

            return Ok(results);
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<RangeContract>>))]
        [EnableRateLimiting(RateLimiterPolicies.fixed_2_limit_in_1_sec)]
        public IActionResult RangeContracts([FromQuery] int contractId)
        {
            var results = _spotService.GetRangeContracts(UserId, contractId);

            return Ok(results);
        }

        [HttpPost("{orderId}")]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public IActionResult CreateOrderV2([FromRoute] string orderId, [FromBody] OrderEmbed model, [FromServices] AmqpService amqpService)
        {
            Log.Information($"CabinetController.CreateOrderV2 orderId={orderId}");

            if (!ModelState.IsValid)
            {
                return BadRequest("Bad data");
            }

            var decode = Encoding.UTF8.GetString(Convert.FromBase64String(model.raw));

            if (string.IsNullOrEmpty(decode))
            {
                return BadRequest("Invalid format");
            }

            var result = JsonConvert.DeserializeObject<OrderForm>(decode);

            if (result == null)
            {
                return BadRequest("Invalid data");
            }

            if (!Guid.TryParse(result.uid, out var uid) || uid == Guid.Empty)
            {
                return BadRequest("Invalid key");
            }

            var serverDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            try
            {
                amqpService.PushMessage(UserId, result.contractId, result.kolvo, result.inp, result.price, GetIPAddress(), result.clientDate, serverDate, result.uid, result.clientVersion, _spotService.LatestToken(), result.dbDate);

                Log.Information($"Order time: {serverDate}; Send order: {JsonConvert.SerializeObject(result, Formatting.None)}");

            }
            catch (Exception exp)
            {
                Log.Error(exp.Message);
            }

            return Ok();
        }

        [HttpPost("{orderId}")]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public IActionResult BulkOrders([FromRoute] string orderId, [FromBody] OrderEmbed model, [FromServices] AmqpService amqpService)
        {
            Log.Information($"CabinetController.BulkOrders orderId={orderId}");

            if (!ModelState.IsValid)
            {
                return BadRequest("Bad data");
            }

            var decode = Encoding.UTF8.GetString(Convert.FromBase64String(model.raw));

            if (string.IsNullOrEmpty(decode))
            {
                return BadRequest("Invalid format");
            }

            var orders = JsonConvert.DeserializeObject<List<OrderForm>>(decode);

            if (orders == null)
            {
                return BadRequest("Invalid data");
            }

            var resultOrders = new List<string>();
            var logOrders = new List<string>();

            for (int i = 0; i < orders.Count; i++)
            {
                if (!Guid.TryParse(orders[i].uid, out var uid) || uid == Guid.Empty)
                {
                    resultOrders.Add($"{orders[i].uid} - ERR: Invalid uid");
                    continue;
                }

                var serverDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                try
                {
                    amqpService.PushMessage(UserId, orders[i].contractId, orders[i].kolvo, orders[i].inp, orders[i].price, GetIPAddress(), orders[i].clientDate, serverDate, orders[i].uid, orders[i].clientVersion, _spotService.LatestToken(), orders[i].dbDate);
                    resultOrders.Add($"{orders[i].uid} - OK");
                    logOrders.Add($"Order time: {serverDate}; Status:OK; Send order: {JsonConvert.SerializeObject(orders[i], Formatting.None)}");
                }
                catch (Exception ex)
                {
                    resultOrders.Add($"{orders[i].uid} - ERR: {ex.Message}");
                    logOrders.Add($"Order time: {serverDate}; Status:ERR; ExceptionMessage:{ex.Message}; Send order: {JsonConvert.SerializeObject(orders[i], Formatting.None)}");
                }
            }

            Task.Factory.StartNew(() =>
            {
                foreach (var log in logOrders)
                {
                    if (log.Contains("ERR"))
                    {
                        Log.Error(log);
                    }
                    else
                    {
                        Log.Information(log);
                    }
                }
            });

            return Ok(resultOrders);
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<OrderTemplate>>))]
        public IActionResult GetOrderTemplates(string search)
        {
            var results = _spotService.GetOrderTemplates(UserId, search);

            return Ok(results);
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public IActionResult CreateOrderTemplate([FromBody] OrderTemplate order)
        {
            var createOrder = _spotService.CreateOrderTemplate(UserId, order);

            if (!createOrder.Success)
                return BadRequest(createOrder.Error);

            return Ok();
        }
    }
}
