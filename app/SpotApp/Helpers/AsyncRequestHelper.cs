using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace SpotApp.Helpers
{
    internal class AsyncRequestHelper
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
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            request.Pipelined = true;
            ServicePoint servicePoint = request.ServicePoint;
            servicePoint.ConnectionLeaseTimeout = int.MaxValue;
            servicePoint.MaxIdleTime = int.MaxValue;
            request.ProtocolVersion = HttpVersion.Version11;
            return request;
        }

        public static void GetAsync(Action<bool, string, Exception> callback, string url, string token = "", int? requestTimeOut = null)
        {
            try
            {
                var request = Create(url);

                request.Method = "GET";

                if (requestTimeOut != null)
                    request.Timeout = requestTimeOut.Value;

                request.ReadWriteTimeout = int.MaxValue;

                if (!string.IsNullOrEmpty(token))
                    request.Headers.Add("Authorization", $"Bearer {token}");

                request.BeginGetResponse(new AsyncCallback(asyncResult =>
                {
                    try
                    {
                        HttpWebRequest req = (HttpWebRequest)asyncResult.AsyncState;
                        using (var response = (HttpWebResponse)req.EndGetResponse(asyncResult))
                        {
                            using (var dataStream = response.GetResponseStream())
                            {
                                using (var reader = new StreamReader(dataStream, Encoding.UTF8))
                                {
                                    var responseStatusCode = (int)response.StatusCode;

                                    if (responseStatusCode >= 200 && responseStatusCode <= 299)
                                    {
                                        callback(true, reader.ReadToEnd(), null);
                                        return;
                                    }

                                    throw new Exception($"StatusCode:{responseStatusCode}; Err:{reader.ReadToEnd()}");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        callback(false, null, ex);
                        return;
                    }
                }), request);
            }
            catch (Exception ex)
            {
                callback(false, null, ex);
                return;
            }
        }

        public static void PostAsync(Action<bool, string, Exception> callback, string url, object data, string token = "", int? requestTimeOut = null)
        {
            try
            {
                var postData = JsonConvert.SerializeObject(data);
                var byteArray = Encoding.UTF8.GetBytes(postData);

                var request = Create(url);

                request.Method = "POST";
                request.ContentLength = byteArray.Length;

                if (requestTimeOut != null)
                    request.Timeout = requestTimeOut.Value;

                request.ReadWriteTimeout = int.MaxValue;

                if (!string.IsNullOrEmpty(token))
                    request.Headers.Add("Authorization", $"Bearer {token}");

                request.BeginGetRequestStream(new AsyncCallback(requestStreamCallback =>
                {
                    try
                    {
                        using (Stream requestStream = request.EndGetRequestStream(requestStreamCallback))
                        {
                            requestStream.Write(byteArray, 0, byteArray.Length);
                        }

                        request.BeginGetResponse(new AsyncCallback(responseCallback =>
                        {
                            try
                            {
                                HttpWebRequest req = (HttpWebRequest)responseCallback.AsyncState;
                                using (HttpWebResponse response = (HttpWebResponse)req.EndGetResponse(responseCallback))
                                {
                                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                                    {
                                        var responseStatusCode = (int)response.StatusCode;

                                        if (responseStatusCode >= 200 && responseStatusCode <= 299)
                                        {
                                            callback(true, reader.ReadToEnd(), null);
                                            return;
                                        }

                                        throw new Exception($"StatusCode:{responseStatusCode}; Err:{reader.ReadToEnd()}");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                callback(false, null, ex);
                                return;
                            }
                        }), request);
                    }
                    catch (Exception ex)
                    {
                        callback(false, null, ex);
                        return;
                    }
                }), request);
            }
            catch (Exception ex)
            {
                callback(false, null, ex);
                return;
            }
        }
    }
}
