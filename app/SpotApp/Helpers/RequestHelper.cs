using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace SpotApp.Helpers
{
    public class RequestHelper
    {
        private static HttpWebRequest Create(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.UserAgent = $"{Environment.OSVersion}";
            request.AllowAutoRedirect = false;
            request.KeepAlive = false;
            request.Proxy = null;
            request.ServicePoint.Expect100Continue = false;

            return request;
        }

        public static string Get(string url, string token = "", int? requestTimeOut = null)
        {
            var request = Create(url);

            request.Method = "GET";

            if (requestTimeOut != null)
                request.Timeout = requestTimeOut.Value;

            if (!string.IsNullOrEmpty(token))
                request.Headers.Add("Authorization", $"Bearer {token}");

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var dataStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(dataStream))
                    {
                        var responseStatusCode = (int)response.StatusCode;

                        if (responseStatusCode >= 200 && responseStatusCode <= 299)
                            return reader.ReadToEnd();

                        throw new Exception($"StatusCode:{responseStatusCode}; Err:{reader.ReadToEnd()}");
                    }
                }
            }
        }

        public static string Post(string url, object data, string token = "", int? requestTimeOut = null)
        {
            var postData = JsonConvert.SerializeObject(data);
            var byteArray = Encoding.UTF8.GetBytes(postData);

            var request = Create(url);

            request.Method = "POST";
            request.ContentLength = byteArray.Length;

            if (requestTimeOut != null)
                request.Timeout = requestTimeOut.Value;

            if (!string.IsNullOrEmpty(token))
                request.Headers.Add("Authorization", $"Bearer {token}");

            using (var reqStream = request.GetRequestStream())
            {
                reqStream.Write(byteArray, 0, byteArray.Length);
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var respStream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(respStream))
                        {
                            var responseStatusCode = (int)response.StatusCode;

                            if (responseStatusCode >= 200 && responseStatusCode <= 299)
                                return reader.ReadToEnd();

                            throw new Exception($"StatusCode:{responseStatusCode}; Err:{reader.ReadToEnd()}");
                        }
                    }
                }
            }
        }
    }
}
