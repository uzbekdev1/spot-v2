using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace SpotApp.Helpers
{
    internal class RequestHelper
    {

        private static HttpWebRequest Create(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.UserAgent = $"{Environment.OSVersion}";
            request.AllowAutoRedirect = false;
            request.KeepAlive = true;
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
                    using (var reader = new StreamReader(dataStream, Encoding.UTF8))
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
                        using (var reader = new StreamReader(respStream, Encoding.UTF8))
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

        public static string GetV2(string url, string token = "", int? requestTimeOut = null)
        {
            var request = Create(url);

            request.Method = "GET";

            if (requestTimeOut != null)
                request.Timeout = requestTimeOut.Value;

            if (!string.IsNullOrEmpty(token))
                request.Headers.Add("Authorization", $"Bearer {token}");

            var response = (HttpWebResponse)request.GetResponse();
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream, Encoding.UTF8);
            var responseStatusCode = (int)response.StatusCode;
            string responseFromServer = reader.ReadToEnd();

            dataStream.Flush();
            dataStream.Close();
            reader.Close();

            if (responseStatusCode >= 200 && responseStatusCode <= 299)
                return responseFromServer;
            throw new Exception($"StatusCode:{responseStatusCode}; Err:{responseFromServer}");
        }

        public static string PostV2(string url, object data, string token = "", int? requestTimeOut = null)
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

            var reqStream = request.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);
            var response = (HttpWebResponse)request.GetResponse();
            var respStream = response.GetResponseStream();
            var reader = new StreamReader(respStream, Encoding.UTF8);
            var responseStatusCode = (int)response.StatusCode;
            string responseFromServer = reader.ReadToEnd();

            respStream.Flush();
            respStream.Close();
            reader.Close();

            if (responseStatusCode >= 200 && responseStatusCode <= 299)
                return responseFromServer;
            throw new Exception($"StatusCode:{responseStatusCode}; Err:{responseFromServer}");
        }
    }
}
