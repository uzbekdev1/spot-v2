using log4net;
using Newtonsoft.Json;
using SpotApp.Core;
using SpotApp.Dtos;
using SpotApp.Helpers;
using SpotApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace SpotApp.Services
{

    internal class SpotService
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void EnableNetFeatures()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
        }

        public SpotService()
        {
            EnableNetFeatures();
        }

        public string GetVersion()
        {
            var client = new WebClient()
            {
                BaseAddress = AppSettings.ApiUrl
            };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";

            var request = client.DownloadData("api/common/checkversion");
            var content = Encoding.UTF8.GetString(request, 0, request.Length);
            var response = JsonConvert.DeserializeObject<ApiResponse<string>>(content);

            if (!response.Success)
            {
                return "";
            }

            return response.Data;
        }

        public UserInfo GetUser(string login, string password)
        {
            var client = new WebClient()
            {
                BaseAddress = AppSettings.ApiUrl
            };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";

            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
            {
                raw = CryptographyHelper.Encrypt(JsonConvert.SerializeObject(new
                {
                    login,
                    password,
                    uid = Guid.NewGuid().ToString()
                }))
            }));
            var request = client.UploadData("api/account/login", "POST", data);
            var content = Encoding.UTF8.GetString(request, 0, request.Length);
            var response = JsonConvert.DeserializeObject<ApiResponse<UserInfo>>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }

            return response.Data;
        }

        public UserInfo GetUserV2(string login, string password)
        {
            try
            {
                var data = new
                {
                    login,
                    password,
                    uid = Guid.NewGuid().ToString()
                };
                var content = RequestHelper.Post($"{AppSettings.ApiUrl}/api/account/login", data);
                var response = JsonConvert.DeserializeObject<ApiResponse<UserInfo>>(content);

                if (!response.Success)
                {
                    throw new Exception(response.Error);
                }

                return response.Data;
            }
            catch
            {
                return null;
            }
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

        public DateTime GetTime(string token)
        {
            var client = new WebClient()
            {
                BaseAddress = AppSettings.ApiUrl
            };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

            var request = client.DownloadData("api/cabinet/checktime");
            if (request.Length == 0)
            {
                throw new Exception("Connection error");
            }

            var content = Encoding.UTF8.GetString(request, 0, request.Length);
            var response = JsonConvert.DeserializeObject<ApiResponse>(content);

            if (!response.Success)
            {
                return DateTime.Now;
            }

            return (DateTime)response.Data;
        }

        public DateTime GetTimeV2(string token)
        {
            var startDate = DateTime.Now;
            string methodStage = "";
            try
            {
                var client = new WebClient()
                {
                    BaseAddress = AppSettings.ApiUrl
                };
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

                var request = client.DownloadData("api/cabinet/checktimev2");
                if (request.Length == 0)
                {
                    throw new Exception("Connection error");
                }

                var content = Encoding.UTF8.GetString(request, 0, request.Length);
                var response = JsonConvert.DeserializeObject<ApiResponse>(content);

                if (!response.Success)
                {
                    return DateTime.Now;
                }

                var tick = Convert.ToDouble(response.Data);

                return new DateTime(1970, 1, 1) + TimeSpan.FromMilliseconds(tick);
            }
            catch (Exception ex)
            {
                _logger.Error($"{methodStage} - {ex.Message}");
                return DateTime.Now;
            }
            finally
            {
                var endDate = DateTime.Now;
                _logger.Info($"PC~SpotService.GetTimeV2 {startDate.ToString("yyyy-MM-dd HH:mm:ss.fff")} - {endDate.ToString("yyyy-MM-dd HH:mm:ss.fff")} diff({endDate.Subtract(startDate).TotalMilliseconds})");
            }
        }

        public DateTime GetTimeV3(string token)
        {
            var startDate = DateTime.Now;
            var methodStage = "";

            try
            {
                var content = RequestHelper.Get($"{AppSettings.ApiUrl}/api/cabinet/checktimev2", token);
                var response = JsonConvert.DeserializeObject<ApiResponse>(content);

                if (!response.Success)
                {
                    return DateTime.Now;
                }

                var tick = Convert.ToDouble(response.Data);

                return new DateTime(1970, 1, 1) + TimeSpan.FromMilliseconds(tick);
            }
            catch (Exception ex)
            {
                _logger.Error($"{methodStage} - {ex.Message}");
                return DateTime.Now;
            }
            finally
            {
                var endDate = DateTime.Now;
                _logger.Info($"PC~SpotService.GetTimeV2 {startDate.ToString("yyyy-MM-dd HH:mm:ss.fff")} - {endDate.ToString("yyyy-MM-dd HH:mm:ss.fff")} diff({endDate.Subtract(startDate).TotalMilliseconds})");
            }
        }

        public void CreateOrder(OrderForm model, string token)
        {
            var client = new WebClient()
            {
                BaseAddress = AppSettings.ApiUrl,
            };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
            {
                raw = CryptographyHelper.Encrypt(JsonConvert.SerializeObject(model))
            }));
            var request = client.UploadData("api/Cabinet/CreateOrder", "POST", data);

            if (request.Length == 0)
            {
                throw new Exception("Connection error");
            }

            var content = Encoding.UTF8.GetString(request, 0, request.Length);
            var response = JsonConvert.DeserializeObject<ApiResponse>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }
        }

        public void DeleteOrder(int orderId, string token)
        {
            var client = new WebClient()
            {
                BaseAddress = AppSettings.ApiUrl
            };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
            {
            }));
            var request = client.UploadData($"api/Cabinet/DeleteOrder/{orderId}", "POST", data);
            if (request.Length == 0)
            {
                throw new Exception("Connection error");
            }

            var content = Encoding.UTF8.GetString(request, 0, request.Length);
            var response = JsonConvert.DeserializeObject<ApiResponse>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }
        }

        public List<MyOrderResult> MyOrders(string token)
        {
            var client = new WebClient()
            {
                BaseAddress = AppSettings.ApiUrl
            };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

            var request = client.DownloadData("api/Cabinet/MyOrders");
            if (request.Length == 0)
            {
                throw new Exception("Connection error");
            }

            var content = Encoding.UTF8.GetString(request, 0, request.Length);
            var response = JsonConvert.DeserializeObject<ApiResponse<List<MyOrderResult>>>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }

            return response.Data;
        }

        public List<ContractItem> AllContracts(string search, string token)
        {
            var client = new WebClient()
            {
                BaseAddress = AppSettings.ApiUrl
            };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

            var request = client.DownloadData($"api/Cabinet/GetContracts?search={search}");
            if (request.Length == 0)
            {
                throw new Exception("Connection error");
            }

            var content = Encoding.UTF8.GetString(request, 0, request.Length);
            var response = JsonConvert.DeserializeObject<ApiResponse<List<ContractItem>>>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }

            return response.Data;
        }

        public List<ClientItem> Clients(string token)
        {
            var client = new WebClient()
            {
                BaseAddress = AppSettings.ApiUrl
            };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

            var request = client.DownloadData("api/Cabinet/GetClients");
            if (request.Length == 0)
            {
                throw new Exception("Connection error");
            }

            var content = Encoding.UTF8.GetString(request, 0, request.Length);
            var response = JsonConvert.DeserializeObject<ApiResponse<List<ClientItem>>>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }

            return response.Data;
        }

        public List<ContactPart> Parts(string token)
        {
            var client = new WebClient()
            {
                BaseAddress = AppSettings.ApiUrl
            };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

            var request = client.DownloadData("api/Cabinet/GetParts");
            if (request.Length == 0)
            {
                throw new Exception("Connection error");
            }

            var content = Encoding.UTF8.GetString(request, 0, request.Length);
            var response = JsonConvert.DeserializeObject<ApiResponse<List<ContactPart>>>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }

            return response.Data;
        }

        public List<OrderItem> AllOrders(int contractId, string token)
        {
            var client = new WebClient()
            {
                BaseAddress = AppSettings.ApiUrl
            };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

            var request = client.DownloadData($"api/Cabinet/GetOrders/{contractId}");
            if (request.Length == 0)
            {
                throw new Exception("Connection error");
            }

            var content = Encoding.UTF8.GetString(request, 0, request.Length);
            var response = JsonConvert.DeserializeObject<ApiResponse<List<OrderItem>>>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }

            return response.Data;
        }

        public List<ContractItem> GetContractsWithId(string search, string token)
        {
            var client = new WebClient()
            {
                BaseAddress = AppSettings.ApiUrl
            };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

            var request = client.DownloadData($"api/Cabinet/GetContractsWithId?search={search}");
            if (request.Length == 0)
            {
                throw new Exception("Connection error");
            }

            var content = Encoding.UTF8.GetString(request, 0, request.Length);
            var response = JsonConvert.DeserializeObject<ApiResponse<List<ContractItem>>>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }

            return response.Data;
        }

        public List<ClientItem> SearchClient(int inp, string token)
        {
            var client = new WebClient()
            {
                BaseAddress = AppSettings.ApiUrl
            };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

            var request = client.DownloadData($"api/Cabinet/SearchClient?inp={inp}");
            if (request.Length == 0)
            {
                throw new Exception("Connection error");
            }

            var content = Encoding.UTF8.GetString(request, 0, request.Length);
            var response = JsonConvert.DeserializeObject<ApiResponse<List<ClientItem>>>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }

            return response.Data;
        }

        public List<ClientItem> SetClient(int inp, string token)
        {
            var client = new WebClient()
            {
                BaseAddress = AppSettings.ApiUrl
            };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

            var request = client.DownloadData($"api/Cabinet/SetClient?inp={inp}");
            if (request.Length == 0)
            {
                throw new Exception("Connection error");
            }

            var content = Encoding.UTF8.GetString(request, 0, request.Length);
            var response = JsonConvert.DeserializeObject<ApiResponse<List<ClientItem>>>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }

            return response.Data;
        }

        public List<ClientItem> RemoveClient(int inp, string token)
        {
            var client = new WebClient()
            {
                BaseAddress = AppSettings.ApiUrl
            };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

            var request = client.DownloadData($"api/Cabinet/RemoveClient?inp={inp}");
            if (request.Length == 0)
            {
                throw new Exception("Connection error");
            }

            var content = Encoding.UTF8.GetString(request, 0, request.Length);
            var response = JsonConvert.DeserializeObject<ApiResponse<List<ClientItem>>>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }

            return response.Data;
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
            var client = new WebClient()
            {
                BaseAddress = AppSettings.ApiUrl
            };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

            var request = client.DownloadData($"api/Cabinet/GetQuotes?contractId={contractId}");
            if (request.Length == 0)
            {
                throw new Exception("Connection error");
            }

            var content = Encoding.UTF8.GetString(request, 0, request.Length);
            var response = JsonConvert.DeserializeObject<ApiResponse<List<Quote>>>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }

            return response.Data;
        }

        public List<RangeContract> RangeContracts(int contractId, string token)
        {
            var client = new WebClient()
            {
                BaseAddress = AppSettings.ApiUrl
            };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

            var request = client.DownloadData($"api/Cabinet/RangeContracts?contractId={contractId}");
            if (request.Length == 0)
            {
                throw new Exception("Connection error");
            }

            var content = Encoding.UTF8.GetString(request, 0, request.Length);
            var response = JsonConvert.DeserializeObject<ApiResponse<List<RangeContract>>>(content);

            if (!response.Success)
            {
                throw new Exception(response.Error);
            }

            return response.Data;
        }

        public void CreateOrderV2(OrderForm model, string token)
        {
            var startDate = DateTime.Now;
            string methodStage = "";
            var modelJs = JsonConvert.SerializeObject(model);
            try
            {
                methodStage = "Create web client";
                var client = new WebClient()
                {
                    BaseAddress = AppSettings.ApiUrl
                };
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

                methodStage = "Create raw";
                var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
                {
                    raw = Convert.ToBase64String(Encoding.UTF8.GetBytes(modelJs))
                }));

                methodStage = "Upload data";
                var request = client.UploadData($"api/Cabinet/CreateOrderV2/{model.uid}", "POST", data);

                methodStage = "Check request";
                if (request.Length == 0)
                {
                    throw new Exception("Connection error");
                }

                //methodStage = "Check content request";
                //var content = Encoding.UTF8.GetString(request, 0, request.Length);

                //methodStage = "Read content";
                //var response = JsonConvert.DeserializeObject<ApiResponse>(content);

                //if (!response.Success)
                //{
                //    methodStage = "Response not success";
                //    throw new Exception(response.Error);
                //}

                var endDate = DateTime.Now;
                _logger.Info($"PC~SpotService.CreateOrderV2 {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds}) - uid: {model.uid}");
            }
            catch (Exception ex)
            {
                var endDate = DateTime.Now;
                _logger.Error($"PC~SpotService.CreateOrderV2 {methodStage} - Err:{ex.Message} {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds}) - uid: {model.uid}");
            }
        }

        public ApiResponse<string> BulkOrders(List<OrderForm> model, string token, string orderLogs)
        {
            var resultBulkOrders = new ApiResponse<string>
            {
                Success = true,
                Data = null,
                Error = null
            };

            var startDate = DateTime.Now;
            string methodStage = "";
            var modelJs = JsonConvert.SerializeObject(model);
            try
            {
                methodStage = "Create web client";
                var client = new WebClient()
                {
                    BaseAddress = AppSettings.ApiUrl
                };
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.Authorization] = $"Bearer {token}";

                methodStage = "Create raw";
                var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
                {
                    raw = Convert.ToBase64String(Encoding.UTF8.GetBytes(modelJs))
                }));

                methodStage = "Upload data";
                var request = client.UploadData($"api/Cabinet/BulkOrders/{model[0].uid}", "POST", data);

                methodStage = "Check request";
                if (request.Length == 0)
                {
                    throw new Exception("Connection error");
                }

                methodStage = "Check content request";
                var content = Encoding.UTF8.GetString(request, 0, request.Length);

                methodStage = "Read content";
                var response = JsonConvert.DeserializeObject<ApiResponse<object>>(content);

                if (!response.Success)
                {
                    resultBulkOrders.Success = false;
                    resultBulkOrders.Error = $"Не успешно - {response.Error}";
                    var endDate = DateTime.Now;
                    _logger.Error($"PC~SpotService.BulkOrders Err:{response.Error} {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds}) - uids: {orderLogs}");
                }
                else
                {
                    resultBulkOrders.Success = true;
                    resultBulkOrders.Data = "Успешно отправлены";
                    var endDate = DateTime.Now;
                    _logger.Info($"PC~SpotService.BulkOrders {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds}) - uids: {orderLogs}");
                }
            }
            catch (Exception ex)
            {
                resultBulkOrders.Success = false;
                resultBulkOrders.Error = $"Не успешно - {ex.Message}";
                var endDate = DateTime.Now;
                _logger.Error($"PC~SpotService.BulkOrders {methodStage} - Err:{ex.Message} {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds}) - uids: {orderLogs}");
            }

            return resultBulkOrders;
        }

    }
}
