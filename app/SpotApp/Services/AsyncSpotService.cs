using log4net;
using Newtonsoft.Json;
using SpotApp.Core;
using SpotApp.Helpers;
using SpotApp.Models;
using System;
using System.Net;
using System.Reflection;
using System.Text;

namespace SpotApp.Services
{
    internal class AsyncSpotService
    {

        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void EnableNetFeatures()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DnsRefreshTimeout = int.MaxValue;
            ServicePointManager.UseNagleAlgorithm = false;
        }

        public AsyncSpotService()
        {
            EnableNetFeatures();
        }

        public void CreateOrderV2Async(OrderForm model, string token)
        {
            var startDate = DateTime.Now;
            string methodStage = "";
            try
            {
                methodStage = "Create raw";
                var data = new { raw = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model))) };

                AsyncRequestHelper.PostAsync((resultIsSuccess, resultData, resultException) =>
                {
                    var endDate = DateTime.Now;
                    if (resultIsSuccess)
                    {
                        _logger.Info($"PC~SpotServiceV2.CreateOrderV2 {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds}) - uid: {model.uid}; result: {resultData}");
                    }
                    else
                    {
                        _logger.Error($"PC~SpotServiceV2.CreateOrderV2 {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds}) - uid: {model.uid}; Err: {resultException.Message}");
                    }
                }, url: $"{AppSettings.ApiUrl}/api/Cabinet/CreateOrderV2/{model.uid}", data: data, token: token);
            }
            catch (Exception ex)
            {
                var endDate = DateTime.Now;
                _logger.Error($"PC~SpotServiceV2.CreateOrderV2 {methodStage} - Err:{ex.Message} {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds}) - uid: {model.uid}");
            }
        }
    }
}
