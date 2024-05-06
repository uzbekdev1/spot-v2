using RestSharp;
using System;

namespace SpotApp.Helpers
{
    public class RequestHelper
    {

        public static string Get(string url, string token = "", int? requestTimeOut = null)
        {
            var client = new RestClient(url);
            client.ConfigureWebRequest((r) =>
            {
                r.ServicePoint.Expect100Continue = false;
                r.KeepAlive = false;
                r.Proxy = null;
                r.AllowAutoRedirect = false;
            });

            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("User-Agent", $"{Environment.OSVersion}");

            if (requestTimeOut != null)
                request.Timeout = requestTimeOut.Value;

            if (!string.IsNullOrEmpty(token))
                request.AddHeader("Authorization", $"Bearer {token}");

            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                return response.Content;
            }
            else
            {
                throw new Exception($"Status: {response.StatusCode}; Error: {response.ErrorMessage}");
            }
        }

        public static string Post(string url, object data, string token = "", int? requestTimeOut = null)
        {
            var client = new RestClient(url);
            client.ConfigureWebRequest((r) =>
            {
                r.ServicePoint.Expect100Continue = false;
                r.KeepAlive = false;
                r.Proxy = null;
                r.AllowAutoRedirect = false;
            });

            var request = new RestRequest(url, Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("User-Agent", $"{Environment.OSVersion}");
            request.AddJsonBody(data);

            if (requestTimeOut != null)
                request.Timeout = requestTimeOut.Value;

            if (!string.IsNullOrEmpty(token))
                request.AddHeader("Authorization", $"Bearer {token}");

            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                return response.Content;
            }
            else
            {
                throw new Exception($"Status: {response.StatusCode}; Error: {response.ErrorMessage}");
            }
        }

    }
}
