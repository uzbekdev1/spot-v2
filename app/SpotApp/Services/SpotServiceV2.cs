using log4net;
using Newtonsoft.Json;
using SpotApp.Core;
using SpotApp.Dtos;
using SpotApp.Exceptions;
using SpotApp.Helpers;
using SpotApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace SpotApp.Services
{
    internal class SpotServiceV2
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void EnableNetFeatures()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
            ServicePointManager.Expect100Continue = false;
        }

        public SpotServiceV2()
        {
            EnableNetFeatures();
        }

        public string GetVersion()
        {
            var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/common/checkversion");
            if (content.Length == 0) throw new Exception("Connection error");
            var response = JsonConvert.DeserializeObject<ApiResponse<string>>(content);

            if (!response.Success)
            {
                return "";
            }

            return response.Data;
        }

        public UserInfo GetUser(string login, string password)
        {
            _logger.Info($"getuser login:{login} create post data");
            var data = new
            {
                raw = CryptographyHelper.Encrypt(JsonConvert.SerializeObject(new
                {
                    login,
                    password,
                    uid = Guid.NewGuid().ToString()
                }))
            };

            _logger.Info($"getuser login:{login} post data");
            var content = RequestHelper.Post($"{AppSettings.ApiUrl}/api/account/login", data);

            if (content.Length == 0) throw new Exception("Connection error");

            _logger.Info($"getuser login:{login} deserialize user data");
            var response = JsonConvert.DeserializeObject<ApiResponse<UserInfo>>(content);

            if (!response.Success)
            {
                _logger.Info($"getuser login:{login} is not success");
                throw new Exception(response.Error);
            }

            return response.Data;
        }

        public bool DownloadLatest(out string executionPath)
        {
            try
            {
                executionPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SpotLauncher.exe");

                if (File.Exists(executionPath))
                {
                    File.Delete(executionPath);
                }

                var client = new WebClient()
                {
                    BaseAddress = AppSettings.ApiUrl
                };
                client.Headers[HttpRequestHeader.ContentType] = "application/octet-stream";

#if DEBUG
                client.DownloadFile("api/common/downloadclient?test=true", executionPath);
#else
                client.DownloadFile("api/common/downloadclient", executionPath);
#endif

                return true;
            }
            catch
            {
                executionPath = "";

                return false;
            }
        }

        public DateTime GetTimeV2(string token)
        {
            string methodStage = "";
            try
            {
                var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/cabinet/checktimev2", token, 3000);
                if (content.Length == 0) throw new Exception("Connection error");
                var response = JsonConvert.DeserializeObject<ApiResponse>(content);

                if (!response.Success)
                {
                    return DateTime.Now;
                }

                var tick = Convert.ToDouble(response.Data);

                return new DateTime(1970, 1, 1) + TimeSpan.FromMilliseconds(tick);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    if (((HttpWebResponse)ex.Response).StatusCode == (HttpStatusCode)429)
                        throw new TooManyRequestsException();

                return DateTime.Now;
            }
            catch (Exception ex)
            {
                _logger.Error($"PC~SpotServiceV2.GetTimeV2 {methodStage} - {ex.Message}");
                return DateTime.Now;
            }
        }

        public void DeleteOrder(int orderId, string token)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { }));

            var content = RequestHelper.Post($"{AppSettings.ApiUrl}/api/Cabinet/DeleteOrder/{orderId}", data, token);
            if (content.Length == 0) throw new Exception("Connection error");
            var response = JsonConvert.DeserializeObject<ApiResponse>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }
        }

        public List<MyOrderResult> MyOrders(string token)
        {
            try
            {
                var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/Cabinet/MyOrders", token);
                if (content.Length == 0) throw new Exception("Connection error");
                var response = JsonConvert.DeserializeObject<ApiResponse<List<MyOrderResult>>>(content);

                if (!response.Success)
                {
                    throw new Exception(response.Error);
                }

                return response.Data;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    if (((HttpWebResponse)ex.Response).StatusCode == (HttpStatusCode)429)
                        throw new TooManyRequestsException();

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AllContract> AllContracts(int partId, string search, bool isProd, string token)
        {
            var startDate = DateTime.Now;
            try
            {
                var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/Cabinet/MainContracts/{partId}?search={search}&isProd={isProd}", token);
                if (content.Length == 0) throw new Exception("Connection error");
                var response = JsonConvert.DeserializeObject<ApiResponse<List<AllContract>>>(content);

                if (!response.Success)
                {
                    throw new Exception(response.Error);
                }

                var endDate = DateTime.Now;
                _logger.Info($"PC~SpotServiceV2.AllContracts {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds})");
                return response.Data;
            }
            catch (Exception ex)
            {
                var endDate = DateTime.Now;
                _logger.Error($"PC~SpotServiceV2.AllContracts - Err:{ex.Message} {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds})");
                return new List<AllContract>();
            }
        }

        public List<SaleContract> SaleContracts(int partId, string search, bool isProd, string token)
        {
            var startDate = DateTime.Now;
            try
            {
                var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/Cabinet/MainContracts/{partId}?search={search}&isProd={isProd}", token);
                if (content.Length == 0) throw new Exception("Connection error");
                var response = JsonConvert.DeserializeObject<ApiResponse<List<SaleContract>>>(content);

                if (!response.Success)
                {
                    throw new Exception(response.Error);
                }

                var endDate = DateTime.Now;
                _logger.Info($"PC~SpotServiceV2.SaleContracts {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds})");
                return response.Data;
            }
            catch (Exception ex)
            {
                var endDate = DateTime.Now;
                _logger.Error($"PC~SpotServiceV2.SaleContracts - Err:{ex.Message} {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds})");
                return new List<SaleContract>();
            }
        }

        public List<ClientItem> Clients(string token)
        {
            try
            {
                var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/Cabinet/GetClients", token);
                if (content.Length == 0) throw new Exception("Connection error");
                var response = JsonConvert.DeserializeObject<ApiResponse<List<ClientItem>>>(content);

                if (!response.Success)
                {
                    throw new Exception(response.Error);
                }

                return response.Data;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    if (((HttpWebResponse)ex.Response).StatusCode == (HttpStatusCode)429)
                        throw new TooManyRequestsException();

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ContactPart> Parts(string token)
        {
            var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/Cabinet/GetParts", token);
            if (content.Length == 0) throw new Exception("Connection error");
            var response = JsonConvert.DeserializeObject<ApiResponse<List<ContactPart>>>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }

            return response.Data;
        }

        public List<OrderItem> AllOrders(int contractId, string token)
        {
            try
            {
                var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/Cabinet/GetOrders/{contractId}", token);
                if (content.Length == 0) throw new Exception("Connection error");
                var response = JsonConvert.DeserializeObject<ApiResponse<List<OrderItem>>>(content);

                if (!response.Success)
                {
                    throw new Exception(response.Error);
                }

                return response.Data;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    if (((HttpWebResponse)ex.Response).StatusCode == (HttpStatusCode)429)
                        throw new TooManyRequestsException();

                throw;
            }
            catch
            {
                throw;
            }
        }

        public List<ContractItem> GetContractsWithId(string search, string token)
        {
            try
            {
                var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/Cabinet/GetContractsWithId?search={search}", token);
                if (content.Length == 0) throw new Exception("Connection error");
                var response = JsonConvert.DeserializeObject<ApiResponse<List<ContractItem>>>(content);

                if (!response.Success)
                {
                    throw new Exception(response.Error);
                }

                return response.Data;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    if (((HttpWebResponse)ex.Response).StatusCode == (HttpStatusCode)429)
                        throw new TooManyRequestsException();

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ClientItem> SearchClient(int inp, string token)
        {
            try
            {
                var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/Cabinet/SearchClient?inp={inp}", token);
                if (content.Length == 0) throw new Exception("Connection error");
                var response = JsonConvert.DeserializeObject<ApiResponse<List<ClientItem>>>(content);

                if (!response.Success)
                {
                    throw new Exception(response.Error);
                }

                return response.Data;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    if (((HttpWebResponse)ex.Response).StatusCode == (HttpStatusCode)429)
                    {
                        _logger.Error(ex.Message);
                        return new List<ClientItem>();
                    }

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ClientItem> SetClient(int inp, string token)
        {
            var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/Cabinet/SetClient?inp={inp}", token);
            if (content.Length == 0) throw new Exception("Connection error");
            var response = JsonConvert.DeserializeObject<ApiResponse<List<ClientItem>>>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }

            return response.Data;
        }

        public List<ClientItem> RemoveClient(int inp, string token)
        {
            var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/Cabinet/RemoveClient?inp={inp}", token);
            if (content.Length == 0) throw new Exception("Connection error");
            var response = JsonConvert.DeserializeObject<ApiResponse<List<ClientItem>>>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }

            return response.Data;
        }

        public List<NewSpotContract> NewSpotMainContracts(string search, string token)
        {
            var startDate = DateTime.Now;
            try
            {
                var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/Cabinet/NewSpotMainContracts?search={search}", token);
                if (content.Length == 0) throw new Exception("Connection error");
                var response = JsonConvert.DeserializeObject<ApiResponse<List<NewSpotContract>>>(content);

                if (!response.Success)
                {
                    throw new Exception(response.Error);
                }

                var endDate = DateTime.Now;
                _logger.Info($"PC~SpotServiceV2.NewSpotMainContracts {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds})");
                return response.Data;
            }
            catch (Exception ex)
            {
                var endDate = DateTime.Now;
                _logger.Error($"PC~SpotServiceV2.NewSpotMainContracts - Err:{ex.Message} {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds})");
                return new List<NewSpotContract>();
            }
        }

        public List<ClientItem> ClientsDDL(string token)
        {
            var dbClients = Clients(token);
            var ddlClients = new List<ClientItem>
            {
                new ClientItem { inp = 0, name = "Выберите клиента" }
            };

            if (dbClients != null)
                ddlClients.AddRange(dbClients);

            return ddlClients;
        }

        public List<Quote> Quotes(int contractId, string token)
        {
            try
            {
                var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/Cabinet/GetQuotes?contractId={contractId}", token);
                if (content.Length == 0) throw new Exception("Connection error");
                var response = JsonConvert.DeserializeObject<ApiResponse<List<Quote>>>(content);

                if (!response.Success)
                {
                    throw new Exception(response.Error);
                }

                return response.Data;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    if (((HttpWebResponse)ex.Response).StatusCode == (HttpStatusCode)429)
                        throw new TooManyRequestsException();

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RangeContract> RangeContracts(int contractId, string token)
        {
            try
            {
                var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/Cabinet/RangeContracts?contractId={contractId}", token);
                if (content.Length == 0) throw new Exception("Connection error");
                var response = JsonConvert.DeserializeObject<ApiResponse<List<RangeContract>>>(content);

                if (!response.Success)
                {
                    throw new Exception(response.Error);
                }

                return response.Data;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    if (((HttpWebResponse)ex.Response).StatusCode == (HttpStatusCode)429)
                        throw new TooManyRequestsException();

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateOrderV2(OrderForm model, string token)
        {
            var startDate = DateTime.Now;
            string methodStage = "";
            try
            {
                methodStage = "Create raw";
                var data = new { raw = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model))) };

                methodStage = "Upload data";
                var content = RequestHelper.Post($"{AppSettings.ApiUrl}/api/Cabinet/CreateOrderV2/{model.uid}", data, token);

                methodStage = "Check request";
                if (content.Length == 0) throw new Exception("Connection error");

                var endDate = DateTime.Now;
                _logger.Info($"PC~SpotServiceV2.CreateOrderV2 {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds}) - uid: {model.uid}");
            }
            catch (Exception ex)
            {
                var endDate = DateTime.Now;
                _logger.Error($"PC~SpotServiceV2.CreateOrderV2 {methodStage} - Err:{ex.Message} {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds}) - uid: {model.uid}");
            }
        }

        public ApiResponse<string> BulkOrders(List<OrderForm> model, string token, string orderLogs, double _timeDifference = 0.0)
        {
            var resultBulkOrders = new ApiResponse<string>
            {
                Success = true,
                Data = null,
                Error = null
            };

            var startDate = DateTime.Now;
            string methodStage = "";
            try
            {
                methodStage = "Create raw";
                var data = new { raw = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model))) };

                methodStage = "Upload data";
                var content = RequestHelper.Post($"{AppSettings.ApiUrl}/api/Cabinet/BulkOrders/{model[0].uid}-{_timeDifference}", data, token);

                if (content.Length == 0) throw new Exception("Connection error");

                methodStage = "Read content";
                var response = JsonConvert.DeserializeObject<ApiResponse<object>>(content);

                if (!response.Success)
                {
                    resultBulkOrders.Success = false;
                    resultBulkOrders.Error = $"Не успешно - {response.Error}";
                    var endDate = DateTime.Now;
                    _logger.Error($"PC~SpotServiceV2.BulkOrders Err:{response.Error} {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds}) - uids: {orderLogs}");
                }
                else
                {
                    resultBulkOrders.Success = true;
                    resultBulkOrders.Data = "Успешно отправлены";
                    var endDate = DateTime.Now;
                    _logger.Info($"PC~SpotServiceV2.BulkOrders {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds}) - uids: {orderLogs}");
                }
            }
            catch (Exception ex)
            {
                resultBulkOrders.Success = false;
                resultBulkOrders.Error = $"Не успешно - {ex.Message}";
                var endDate = DateTime.Now;
                _logger.Error($"PC~SpotServiceV2.BulkOrders {methodStage} - Err:{ex.Message} {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds}) - uids: {orderLogs}");
            }

            return resultBulkOrders;
        }

        public List<OrderTemplate> GetOrderTemplates(string serach, string token)
        {
            var startDate = DateTime.Now;
            try
            {
                var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/Cabinet/GetOrderTemplates?search={serach}", token);
                if (content.Length == 0) throw new Exception("Connection error");
                var response = JsonConvert.DeserializeObject<ApiResponse<List<OrderTemplate>>>(content);

                if (!response.Success)
                {
                    throw new Exception(response.Error);
                }

                var endDate = DateTime.Now;
                _logger.Info($"PC~SpotServiceV2.GetOrderTemplates {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds})");
                return response.Data;
            }
            catch (Exception ex)
            {
                var endDate = DateTime.Now;
                _logger.Error($"PC~SpotServiceV2.GetOrderTemplates - Err:{ex.Message} {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds})");
                return new List<OrderTemplate>();
            }
        }

        public ApiResponse CreateOrderTemplate(OrderTemplate order, string token)
        {
            var content = RequestHelper.Post($"{AppSettings.ApiUrl}/api/Cabinet/CreateOrderTemplate", order, token, 3000);

            if (content.Length == 0)
                return new ApiResponse { Success = false, Error = "Connection error" };

            var response = JsonConvert.DeserializeObject<ApiResponse>(content);
            return response;
        }
    }
}
