using ClientApi.Core;
using ClientApi.Dtos;
using Newtonsoft.Json;
using RestSharp;

namespace ClientApi.Services
{
    public class NewSpotService
    {

        public readonly string _apiUrl;

        public NewSpotService(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public IEnumerable<NewSpotMainContact> MainContracts(string search)
        {
            var client = new RestClient(_apiUrl);
            var request = new RestRequest($"/Contract/GetSellBids?search={search}&type=0", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute(request);

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
