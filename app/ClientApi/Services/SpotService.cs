using ClientApi.Core;
using ClientApi.Dtos;
using Newtonsoft.Json;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;

namespace ClientApi.Services
{
    public class SpotService
    {

        private string _apiKey;

        public readonly string _apiUrl;

        private static bool ValidationAccessToken(string token)
        {
            JwtSecurityToken jwtSecurityToken;

            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);
            }
            catch (Exception)
            {
                return false;
            }

            return jwtSecurityToken.ValidTo > DateTime.Now;
        }

        private string GetToken()
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/user/authenticate", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(new
            {
                username = "test",
                password = "test"
            });

            var response = client.Execute(request);

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<string>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

        public SpotService(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public void RenewToken()
        {
            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                _apiKey = GetToken();
            }
            else
            {
                if (!ValidationAccessToken(_apiKey))
                {
                    _apiKey = GetToken();
                }
            }
        }

        public string LatestToken()
        {
            return _apiKey;
        }

        public DateTime GetDate()
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/time-now", Method.Get);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            var response = client.Execute(request);

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

        public UserResponse GetUser(string username, string password, string newId)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/gettraderdata", Method.Post);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            request.AddJsonBody(new
            {
                username,
                password,
                newId
            });

            var response = client.Execute(request);

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

        public UserResponse GetUser(int id)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest($"/api/spot/gettraderdata/{id}", Method.Get);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            var response = client.Execute(request);

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

        public IEnumerable<ContactItem> GetContracts(string search)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getcontracts", Method.Post);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            request.AddJsonBody(new
            {
                search
            });

            var response = client.Execute(request);

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

        public IEnumerable<MainContact> MainContracts(int traderId, int partId, string search, bool isProd)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getpartcontracts", Method.Post);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            request.AddJsonBody(new
            {
                traderId,
                partId,
                search,
                isprod = isProd ? 1 : 0
            });

            var response = client.Execute(request);

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

        public IEnumerable<MyOrderResult> MyOrders(int traderId)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/myorders", Method.Post);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            request.AddJsonBody(new
            {
                traderId
            });

            var response = client.Execute(request);

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

        public IEnumerable<OrderItem> GetOrders(int contractId, int traderId)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("api/spot/orderswithconrtact", Method.Post);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            request.AddJsonBody(new
            {
                contractId,
                traderId
            });

            var response = client.Execute(request);

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

        public void DeleteOrder(int orderId, int traderId, string traderIp)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("api/Spot/deleteorder", Method.Post);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            request.AddJsonBody(new
            {
                orderId,
                traderId,
                traderIp
            });

            var response = client.Execute(request);

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

        public IEnumerable<PetroClient> GetClients(int traderId)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getclients", Method.Post);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            request.AddJsonBody(new
            {
                traderId,
            });

            var response = client.Execute(request);

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

        public IEnumerable<ContractPart> GetParts()
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getcontractparts");

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            var response = client.Execute(request);

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

        public IEnumerable<ContactItem> GetContractsWithId(string search)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getcontractswithid", Method.Post);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            request.AddJsonBody(new
            {
                search
            });

            var response = client.Execute(request);

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

        public IEnumerable<PetroClient> SearchClient(int traderId, int inp)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/searchclient", Method.Post);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            request.AddJsonBody(new
            {
                traderId,
                inp
            });

            var response = client.Execute(request);

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

        public IEnumerable<PetroClient> SetClient(int traderId, int inp)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/setclient", Method.Post);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            request.AddJsonBody(new
            {
                traderId,
                inp
            });

            var response = client.Execute(request);

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

        public IEnumerable<PetroClient> RemoveClient(int traderId, int inp)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/removeclient", Method.Post);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            request.AddJsonBody(new
            {
                traderId,
                inp
            });

            var response = client.Execute(request);

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

        public IEnumerable<Quote> GetQuotes(int traderId, int contractId)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getquote", Method.Post);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            request.AddJsonBody(new
            {
                traderId,
                contractId
            });

            var response = client.Execute(request);

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

        public IEnumerable<RangeContract> GetRangeContracts(int traderId, int contractId)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getrangecontract", Method.Post);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            request.AddJsonBody(new
            {
                traderId,
                contractId
            });

            var response = client.Execute(request);

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

        public IEnumerable<OrderTemplate> GetOrderTemplates(int traderId, string search)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/getordertemplate", Method.Post);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            request.AddJsonBody(new
            {
                traderId
            });

            var response = client.Execute(request);

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

        public ApiResponse CreateOrderTemplate(int userId, OrderTemplate order)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("api/Spot/createordertemplate", Method.Post);

            RenewToken();

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            request.AddJsonBody(new
            {
                TraderId = userId,
                ContractId = order.contractId,
                Inp = order.inp,
                Price = order.price,
                Kolvo = order.kolvo,
                MaxPriceCount = order.maxPriceCount
            });

            var response = client.Execute(request);

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                return new ApiResponse { Success = false, Error = "Server error", Data = null };
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse>(response.Content);

            return resut;
        }
    }
}