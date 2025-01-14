using ClientApi.Core;
using ClientApi.Dtos;
using Newtonsoft.Json;
using RestSharp;

namespace ClientApi.Services
{
    public class NewSpotService
    {

        private readonly TimeSpan _timeOut = TimeSpan.FromMilliseconds(6 * 1000);

        public readonly string _apiUrl;

        public NewSpotService(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public async Task<IEnumerable<NewSpotMainContact>> MainContracts(string search)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest($"/Contract/GetSellBids?search={search}&type=0", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.Timeout = _timeOut;

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"{response.ErrorException?.Message} {response.ResponseStatus}");
            }

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Server error");
            }

            var resut = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<NewSpotMainContact>>>(response.Content);

            if (!resut.Success)
            {
                throw new Exception(resut.Error);
            }

            return resut.Data;
        }

    }
}
