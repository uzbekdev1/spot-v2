using ClientApi.Core;
using ClientApi.Dtos;
using ClientApi.Enums;
using ClientApi.Filters;
using ClientApi.Helpers;
using ClientApi.Models;
using ClientApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace ClientApi.Controllers
{
    [CheckAuth]
    public class CabinetController : BaseController
    {

        private readonly SpotService _spotService;

        private readonly NewSpotService _newSpotService;

        private readonly CryptographyHelper _cryptographyHelper;

        private readonly ILogger<CabinetController> _logger;

        public CabinetController(SpotService spotService, NewSpotService newSpotService, CryptographyHelper cryptographyHelper, ILogger<CabinetController> logger)
        {
            _spotService = spotService;
            _newSpotService = newSpotService;
            _cryptographyHelper = cryptographyHelper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<double>))]
        public IActionResult CheckTimeV2()
        {
            var timeNow = DateTime.Now;
            var milliSeconds = timeNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;

            Task.Factory.StartNew(() =>
            {
                _logger.LogInformation($"CheckTimeV2: {timeNow.ToString("yyyy-MM-dd HH:mm:ss.fff")}; UserId:{UserId}; UserIp:{GetIPAddress}");
            });

            return Ok(milliSeconds);
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<ContactItem>>))]
        public async Task<IActionResult> GetContracts([FromQuery] string search)
        {
            try
            {
                var items = await _spotService.GetContracts(search);

                return Ok(items);
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { search }));
                return BadRequest(exp.Message);
            }
        }

        [HttpGet("{partId}")]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<MainContact>>))]
        public async Task<IActionResult> MainContracts([FromRoute] int partId, [FromQuery] string search = "", [FromQuery] bool isProd = false)
        {
            try
            {
                var items = await _spotService.MainContracts(UserId, partId, search, isProd);

                return Ok(items);
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { UserId, partId, search, isProd }));
                return BadRequest(exp.Message);
            }
        }

        [HttpPost("{orderId}")]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<IActionResult> DeleteOrder([FromRoute] int orderId)
        {
            try
            {
                await _spotService.DeleteOrder(orderId, UserId, GetIPAddress());
                return Ok();
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { orderId }));
                return BadRequest(exp.Message);
            }
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<MyOrderResult>>))]
        public async Task<IActionResult> MyOrders()
        {
            try
            {
                var results = await _spotService.MyOrders(UserId);

                return Ok(results);
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { UserId }));
                return BadRequest(exp.Message);
            }
        }

        [HttpGet("{contractId}")]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<OrderItem>>))]
        public async Task<IActionResult> GetOrders([FromRoute] int contractId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Bad data");
                }

                var results = await _spotService.GetOrders(contractId, UserId);

                foreach (var item in results)
                {
                    item.mine = item.traderid == UserId;
                }

                return Ok(results);
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { contractId, UserId }));
                return BadRequest(exp.Message);
            }
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<PetroClient>>))]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                var results = await _spotService.GetClients(UserId);

                return Ok(results);
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { UserId }));
                return BadRequest(exp.Message);
            }
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<ContractPart>>))]
        public async Task<IActionResult> GetParts()
        {
            try
            {
                var results = await _spotService.GetParts();

                return Ok(results);
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { }));
                return BadRequest(exp.Message);
            }
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<ContactItem>>))]
        public async Task<IActionResult> GetContractsWithId([FromQuery, BindRequired, MaxLength(9)] string search)
        {
            try
            {
                var items = await _spotService.GetContractsWithId(search);

                return Ok(items);
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { search }));
                return BadRequest(exp.Message);
            }
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<PetroClient>>))]
        public async Task<IActionResult> SearchClient([FromQuery] int inp)
        {
            try
            {
                var results = await _spotService.SearchClient(UserId, inp);

                return Ok(results);
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { UserId, inp }));
                return BadRequest(exp.Message);
            }
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<PetroClient>>))]
        public async Task<IActionResult> SetClient([FromQuery] int inp)
        {
            try
            {
                var results = await _spotService.SetClient(UserId, inp);

                return Ok(results);
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { UserId, inp }));
                return BadRequest(exp.Message);
            }
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<PetroClient>>))]
        public async Task<IActionResult> RemoveClient([FromQuery] int inp)
        {
            try
            {
                var results = await _spotService.RemoveClient(UserId, inp);

                return Ok(results);
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { UserId, inp }));
                return BadRequest(exp.Message);
            }
        }

        [HttpGet()]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<MainContact>>))]
        public async Task<IActionResult> NewSpotMainContracts([FromQuery] string search = "")
        {
            try
            {
                var items = await _newSpotService.MainContracts(search);
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
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { search }));
                return BadRequest(exp.Message);
            }
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<Quote>>))]
        public async Task<IActionResult> GetQuotes([FromQuery] int contractId)
        {
            try
            {
                var results = await _spotService.GetQuotes(UserId, contractId);

                return Ok(results);
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { UserId, contractId }));
                return BadRequest(exp.Message);
            }
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<RangeContract>>))]
        public async Task<IActionResult> RangeContracts([FromQuery] int contractId)
        {
            try
            {
                var results = await _spotService.GetRangeContracts(UserId, contractId);

                return Ok(results);
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { UserId, contractId }));
                return BadRequest(exp.Message);
            }
        }

        [HttpPost("{orderId}")]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public IActionResult CreateOrderV2([FromRoute] string orderId, [FromBody] OrderEmbed model, [FromServices] AmqpService amqpService)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Bad data");
                }

                var decode = Encoding.UTF8.GetString(Convert.FromBase64String(model.raw));

                if (string.IsNullOrEmpty(decode))
                {
                    throw new Exception("Invalid format");
                }

                var result = JsonConvert.DeserializeObject<OrderForm>(decode);

                if (result == null)
                {
                    throw new Exception("Invalid data");
                }

                if (!Guid.TryParse(result.uid, out var uid) || uid == Guid.Empty)
                {
                    throw new Exception("Invalid key");
                }

                if (!int.TryParse(_cryptographyHelper.DecryptV2(result.kolvoStr), out int _kolvo))
                {
                    throw new Exception("Invalid amount");
                }

                result.kolvo = _kolvo;

                if (!decimal.TryParse(_cryptographyHelper.DecryptV2(result.priceStr).CleanAsDecimal(), NumberStyles.Any, null, out decimal _price))
                {
                    throw new Exception("Invalid price");
                }

                result.price = _price;

                var serverDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                try
                {
                    amqpService.PushMessage(UserId, result.contractId, result.kolvo, result.inp, result.price, GetIPAddress(), result.clientDate, serverDate, result.uid, result.clientVersion, "", result.dbDate, (int)OrderTypes.CreateOrderV2);
                    stopWatch.Stop();
                    _logger.LogInformation($"CreateOrderV2 serverDate: {serverDate}; UserId: {UserId}; totalMilliseconds:{stopWatch.Elapsed.TotalMilliseconds} msec; Send order: {JsonConvert.SerializeObject(result, Formatting.None)}");
                }
                catch (Exception exp)
                {
                    if (stopWatch.IsRunning)
                        stopWatch.Stop();

                    _logger.LogError(LogGenerate.Instance.GenerateLogError("CreateOrderV2", $"{exp.Message}; totalMilliseconds:{stopWatch.Elapsed.TotalMilliseconds} msec;", exp.InnerException, exp.StackTrace, new { UserId, result }));
                }

                return Ok();
            }
            catch (Exception exp)
            {
                if (stopWatch.IsRunning)
                    stopWatch.Stop();

                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{exp.Message}; totalMilliseconds:{stopWatch.Elapsed.TotalMilliseconds} msec;", exp.InnerException, exp.StackTrace, new { UserId, orderId, model }));
                return BadRequest(exp.Message);
            }
            finally
            {
                if (stopWatch.IsRunning)
                    stopWatch.Stop();
            }
        }

        [HttpPost("{orderId}")]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public IActionResult BulkOrders([FromRoute] string orderId, [FromBody] OrderEmbed model, [FromServices] AmqpService amqpService)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                _logger.LogInformation($"CabinetController.BulkOrders orderId={orderId}");

                if (!ModelState.IsValid)
                {
                    throw new Exception("Bad data");
                }

                var decode = Encoding.UTF8.GetString(Convert.FromBase64String(model.raw));

                if (string.IsNullOrEmpty(decode))
                {
                    throw new Exception("Invalid format");
                }

                var orders = JsonConvert.DeserializeObject<List<OrderForm>>(decode);

                if (orders == null || orders.Count == 0)
                {
                    throw new Exception("Invalid data");
                }

                var resultOrders = new List<string>();
                var logOrders = new List<string>();

                var _userIp = GetIPAddress();
                var _userId = UserId;

                for (int i = 0; i < orders.Count; i++)
                {
                    if (!Guid.TryParse(orders[i].uid, out var uid) || uid == Guid.Empty)
                    {
                        resultOrders.Add($"{orders[i].uid} - ERR: Invalid uid");
                        continue;
                    }

                    if (!int.TryParse(_cryptographyHelper.DecryptV2(orders[i].kolvoStr), out int _kolvo))
                    {
                        resultOrders.Add($"{orders[i].uid} - ERR: Invalid amount");
                        continue;
                    }

                    orders[i].kolvo = _kolvo;

                    if (!decimal.TryParse(_cryptographyHelper.DecryptV2(orders[i].priceStr).CleanAsDecimal(), NumberStyles.Any, null, out decimal _price))
                    {
                        resultOrders.Add($"{orders[i].uid} - ERR: Invalid price");
                        continue;
                    }

                    orders[i].price = _price;

                    var serverDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                    try
                    {
                        amqpService.PushMessage(_userId, orders[i].contractId, orders[i].kolvo, orders[i].inp, orders[i].price, _userIp, orders[i].clientDate, serverDate, orders[i].uid, orders[i].clientVersion, "", orders[i].dbDate, (int)OrderTypes.BulkOrder);
                        resultOrders.Add($"{orders[i].uid} - OK");
                        logOrders.Add($"BulkOrders serverDate: {serverDate}; Status:OK; Send order: {JsonConvert.SerializeObject(orders[i], Formatting.None)}");
                    }
                    catch (Exception ex)
                    {
                        resultOrders.Add($"{orders[i].uid} - ERR: {ex.Message}");
                        logOrders.Add($"BulkOrders serverDate: {serverDate}; Status:ERR; ExceptionMessage:{ex.Message}; Send order: {JsonConvert.SerializeObject(orders[i], Formatting.None)}");
                    }
                }

                stopWatch.Stop();

                var totalMilliseconds = stopWatch.Elapsed.TotalMilliseconds;

                Task.Factory.StartNew(() =>
                {
                    foreach (var log in logOrders)
                    {
                        if (log.Contains("ERR"))
                        {
                            _logger.LogError($"totalMilliseconds: {totalMilliseconds} msec; {log}");
                        }
                        else
                        {
                            _logger.LogInformation($"totalMilliseconds: {totalMilliseconds} msec; {log}");
                        }
                    }
                });

                return Ok(resultOrders);
            }
            catch (Exception exp)
            {
                if (stopWatch.IsRunning)
                    stopWatch.Stop();

                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{exp.Message}; totalMilliseconds:{stopWatch.Elapsed.TotalMilliseconds} msec;", exp.InnerException, exp.StackTrace, new { UserId, orderId, model }));
                return BadRequest(exp.Message);
            }
            finally
            {
                if (stopWatch.IsRunning)
                    stopWatch.Stop();
            }
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<OrderTemplate>>))]
        public async Task<IActionResult> GetOrderTemplates(string search)
        {
            try
            {
                var results = await _spotService.GetOrderTemplates(UserId, search);

                return Ok(results);
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { UserId, search }));
                return BadRequest(exp.Message);
            }
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<IActionResult> CreateOrderTemplate([FromBody] OrderTemplate order)
        {
            try
            {
                var createOrder = await _spotService.CreateOrderTemplate(UserId, order);

                if (!createOrder.Success)
                    return BadRequest(createOrder.Error);

                return Ok();
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { UserId, order }));
                return BadRequest(exp.Message);
            }
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<IActionResult> DeleteOrderTemplate([FromBody] int templateId)
        {
            try
            {
                var createOrder = await _spotService.DeleteOrderTemplate(UserId, templateId);

                if (!createOrder.Success)
                    return BadRequest(createOrder.Error);

                return Ok();
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { UserId, templateId }));
                return BadRequest(exp.Message);
            }
        }

        [HttpPost("{orderId}")]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public IActionResult CreatePostOrderV2([FromRoute] string orderId, [FromBody] OrderEmbed model, [FromServices] AmqpService amqpService)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Bad data");
                }

                var decode = Encoding.UTF8.GetString(Convert.FromBase64String(model.raw));

                if (string.IsNullOrEmpty(decode))
                {
                    throw new Exception("Invalid format");
                }

                var result = JsonConvert.DeserializeObject<OrderForm>(decode);

                if (result == null)
                {
                    throw new Exception("Invalid data");
                }

                if (!Guid.TryParse(result.uid, out var uid) || uid == Guid.Empty)
                {
                    throw new Exception("Invalid key");
                }

                var serverDate = DateTime.Now;

                DateTime? postDate = DateTime.TryParse(result.serverDate, out var _postDate) ? _postDate : null;

                if (!postDate.HasValue)
                {
                    throw new Exception("Время подачи не введен");
                }

                if (serverDate.Year != postDate.Value.Year || serverDate.Month != postDate.Value.Month || serverDate.Day != postDate.Value.Day)
                {
                    throw new Exception($"Неправильный время подачи {postDate.Value:dd.MM.yyyy HH:mm:ss.fff}");
                }

                if (postDate.Value < serverDate)
                {
                    throw new Exception($"Неправильный время подачи {postDate.Value:dd.MM.yyyy HH:mm:ss.fff} < Сервер время {serverDate:dd.MM.yyyy HH:mm:ss.fff}");
                }

                if (!int.TryParse(_cryptographyHelper.DecryptV2(result.kolvoStr), out int _kolvo))
                {
                    throw new Exception("Invalid amount");
                }

                result.kolvo = _kolvo;

                if (!decimal.TryParse(_cryptographyHelper.DecryptV2(result.priceStr).CleanAsDecimal(), NumberStyles.Any, null, out decimal _price))
                {
                    throw new Exception("Invalid price");
                }

                result.price = _price;

                try
                {
                    amqpService.PushMessage(UserId, result.contractId, result.kolvo, result.inp, result.price, GetIPAddress(), result.clientDate, postDate.Value.ToString("yyyy-MM-dd HH:mm:ss.fff"), result.uid, result.clientVersion, "", result.dbDate, (int)OrderTypes.CreatePostOrderV2);
                    stopWatch.Stop();
                    _logger.LogInformation($"CreatePostOrderV2 serverDate: {serverDate:yyyy-MM-dd HH:mm:ss.fff}; Post Date: {postDate.Value:yyyy-MM-dd HH:mm:ss.fff} UserId: {UserId}; totalMilliseconds:{stopWatch.Elapsed.TotalMilliseconds} msec; Send order: {JsonConvert.SerializeObject(result, Formatting.None)}");
                }
                catch (Exception exp)
                {
                    if (stopWatch.IsRunning)
                        stopWatch.Stop();

                    _logger.LogError(LogGenerate.Instance.GenerateLogError("CreatePostOrderV2", $"{exp.Message}; totalMilliseconds:{stopWatch.Elapsed.TotalMilliseconds} msec;", exp.InnerException, exp.StackTrace, new { UserId, result }));
                }

                return Ok();
            }
            catch (Exception exp)
            {
                if (stopWatch.IsRunning)
                    stopWatch.Stop();

                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{exp.Message}; totalMilliseconds:{stopWatch.Elapsed.TotalMilliseconds} msec;", exp.InnerException, exp.StackTrace, new { UserId, orderId, model }));
                return BadRequest(exp.Message);
            }
            finally
            {
                if (stopWatch.IsRunning)
                    stopWatch.Stop();
            }
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse<List<BargainsModel>>))]
        public async Task<IActionResult> GetBargains()
        {
            try
            {
                var result = await _spotService.GetBargains(UserId);
                return Ok(result);
            }
            catch (Exception exp)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", exp.Message, exp.InnerException, exp.StackTrace, new { UserId }));
                return BadRequest(exp.Message);
            }
        }

    }
}
