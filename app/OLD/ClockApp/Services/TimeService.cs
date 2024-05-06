using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace ClockApp.Services
{
    internal class TimeService
    {
        private static void EnableNetFeatures()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
        }

        public TimeService()
        {
            EnableNetFeatures();
        }

        public DateTime GetTime()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://time.uzex.uz/Home/Index");
                request.Method = "GET";
                request.AllowAutoRedirect = false;
                request.KeepAlive = false;
                request.UserAgent =$"{Environment.OSVersion}"; 

                request.Accept = "application/json";
                request.ContentType = "application/json";
                request.Headers["X-Requested-With"] = "XMLHttpRequest";

                var response = (HttpWebResponse)request.GetResponse();
                var dataStream = response.GetResponseStream();
                var reader = new StreamReader(dataStream);
                var content = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                var result = JsonConvert.DeserializeObject<long>(content);
                var dt = new DateTime(1970, 1, 1) + TimeSpan.FromMilliseconds(result);

                return dt.ToLocalTime();
            }
            catch
            {
                return DateTime.Now;
            }
        }
    }
}
