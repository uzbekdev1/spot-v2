using ClientApi.Core;
using ClientApi.Dtos;
using ClientApi.Helpers;
using Newtonsoft.Json;
using RestSharp;

namespace ClientApi.Services
{
    public class SpotService
    {
        private readonly int _apiTimedOut = 5000; // 5000 msec = 5 sec

        public readonly string _apiUrl;

        private readonly ILogger<SpotService> _logger;

        public SpotService(string apiUrl, ILogger<SpotService> logger)
        {
            _apiUrl = apiUrl;
            _logger = logger;
        }

        public async Task<DateTime> GetDate()
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/time-now", Method.Get);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<DateTime>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public async Task<UserResponse> GetUser(string username, string password, string newId)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/gettraderdata", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                username,
                password,
                newId
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { username, password, newId }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<UserResponse>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public async Task<UserResponse> GetUser(int id)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest($"/api/spot/gettraderdata/{id}", Method.Get);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { id }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<UserResponse>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public async Task<IEnumerable<ContactItem>> GetContracts(string search)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getcontracts", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                search
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { search }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<ContactItem>>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public async Task<IEnumerable<MainContact>> MainContracts(int traderId, int partId, string search, bool isProd)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getpartcontracts", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                traderId,
                partId,
                search,
                isprod = isProd ? 1 : 0
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { traderId, partId, search, isProd }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<MainContact>>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public async Task<IEnumerable<MyOrderResult>> MyOrders(int traderId)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/myorders", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                traderId
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { traderId }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<MyOrderResult>>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public async Task<IEnumerable<OrderItem>> GetOrders(int contractId, int traderId)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("api/spot/orderswithconrtact", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                contractId,
                traderId
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { contractId, traderId }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<OrderItem>>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public async Task DeleteOrder(int orderId, int traderId, string traderIp)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("api/Spot/deleteorder", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                orderId,
                traderId,
                traderIp
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { orderId, traderId, traderIp }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }
        }

        public async Task<IEnumerable<PetroClient>> GetClients(int traderId)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getclients", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                traderId,
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { traderId }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<PetroClient>>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public async Task<IEnumerable<ContractPart>> GetParts()
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getcontractparts");

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<ContractPart>>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public async Task<IEnumerable<ContactItem>> GetContractsWithId(string search)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getcontractswithid", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                search
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { search }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<ContactItem>>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public async Task<IEnumerable<PetroClient>> SearchClient(int traderId, int inp)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/searchclient", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                traderId,
                inp
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { traderId, inp }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<PetroClient>>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public async Task<IEnumerable<PetroClient>> SetClient(int traderId, int inp)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/setclient", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                traderId,
                inp
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { traderId, inp }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<PetroClient>>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public async Task<IEnumerable<PetroClient>> RemoveClient(int traderId, int inp)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/removeclient", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                traderId,
                inp
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { traderId, inp }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<PetroClient>>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public async Task<IEnumerable<Quote>> GetQuotes(int traderId, int contractId)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getquote", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                traderId,
                contractId
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { traderId, contractId }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<Quote>>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public async Task<IEnumerable<RangeContract>> GetRangeContracts(int traderId, int contractId)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getrangecontract", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                traderId,
                contractId
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { traderId, contractId }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<RangeContract>>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public async Task<IEnumerable<OrderTemplate>> GetOrderTemplates(int traderId, string search)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getordertemplate", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                traderId
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { traderId, search }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<OrderTemplate>>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public async Task<ApiResponse> CreateOrderTemplate(int userId, OrderTemplate order)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("api/Spot/createordertemplate", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                TraderId = userId,
                ContractId = order.contractId,
                Inp = order.inp,
                Price = order.price,
                Kolvo = order.kolvo,
                MaxPriceCount = order.maxPriceCount
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { userId, order }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                return new ApiResponse { Success = false, Error = "Server error", Data = null };
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse>(response.Content);

            return resut;
        }

        public async Task<ApiResponse> DeleteOrderTemplate(int traderId, int templateId)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("api/Spot/deleteordertemplate", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                TraderId = traderId,
                Id = templateId
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { traderId, templateId }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                return new ApiResponse { Success = false, Error = "Server error", Data = null };
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse>(response.Content);

            return resut;
        }

        public async Task<IEnumerable<BargainsModel>> GetBargains(int traderId)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getbargains", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _apiTimedOut;

            request.AddJsonBody(new
            {
                traderId
            });

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError(LogGenerate.Instance.GenerateLogError("", $"{response.ErrorException?.Message} {response.ResponseStatus}", response.ErrorException?.InnerException, response.ErrorException?.StackTrace, new { traderId }));
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<BargainsModel>>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }
    }
}