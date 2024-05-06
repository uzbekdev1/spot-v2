using log4net;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace ClockApp.Services
{
    internal class TimeService
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void EnableNetFeatures()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
            ServicePointManager.Expect100Continue = false;
        }

        public TimeService()
        {
            EnableNetFeatures();
        }

        public DateTime GetTime(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";
                request.AllowAutoRedirect = false;
                request.KeepAlive = false;
                request.UserAgent = $"{Environment.OSVersion}";

                request.Accept = "application/json";
                request.ContentType = "application/json";
                request.Headers["X-Requested-With"] = "XMLHttpRequest";

                request.Timeout = 3000; //time out 3 sec.

                request.Proxy = null;
                request.ServicePoint.Expect100Continue = false;

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var dataStream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(dataStream))
                        {
                            var content = reader.ReadToEnd();

                            var result = JsonConvert.DeserializeObject<long>(content);
                            var dt = new DateTime(1970, 1, 1) + TimeSpan.FromMilliseconds(result);

                            return dt.ToLocalTime();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"PC~TimeService.GetTime Err: {ex.Message}");
                return DateTime.Now;
            }
        }
    }
}
