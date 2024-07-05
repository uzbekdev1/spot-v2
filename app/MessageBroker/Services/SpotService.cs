using MessageBroker.Core;
using Newtonsoft.Json;
using RestSharp;

namespace MessageBroker.Services
{
    public class SpotService
    {
        public readonly string _apiUrl;

        public SpotService(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public void CreateOrder(int traderId, int contractId, int kolvo, int inp, decimal price, string ip, string clientDate, string serverDate, string jobDate, string newId, string serverHost, string clientVersion, string dbDate)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/createorder", Method.Post)
            {
                Timeout = 10000
            };

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(new
            {
                traderId,
                contractId,
                kolvo,
                inp,
                price,
                ip,
                clientDate,
                serverDate,
                jobDate,
                newId,
                serverHost,
                clientVersion,
                dbDate
            });

            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}; InnerException: {response.ErrorException?.InnerException}; StackTrace: {response.ErrorException?.StackTrace}");
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

        public void CreatePostOrder(int traderId, int contractId, int kolvo, int inp, decimal price, string ip, string clientDate, string serverDate, string jobDate, string newId, string serverHost, string clientVersion, string dbDate)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/CreateOrderByDate", Method.Post)
            {
                Timeout = 10000
            };

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(new
            {
                traderId,
                contractId,
                kolvo,
                inp,
                price,
                ip,
                clientDate,
                serverDate,
                jobDate,
                newId,
                serverHost,
                clientVersion,
                dbDate
            });

            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}; InnerException: {response.ErrorException?.InnerException}; StackTrace: {response.ErrorException?.StackTrace}");
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
    }
}