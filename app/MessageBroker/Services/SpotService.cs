using MessageBroker.Core;
using Newtonsoft.Json;
using RestSharp;

namespace MessageBroker.Services
{
    public class SpotService
    {

        private string _apiKey;

        public readonly string _apiUrl;

        public SpotService(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public void SetToken(string token)
        {
            _apiKey = token;
        }

        public void CreateOrder(int traderId, int contractId, int kolvo, int inp, decimal price, string ip, string clientDate, string serverDate, string jobDate, string newId, string serverHost, string clientVersion, string dbDate)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("/api/spot/createorder", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

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