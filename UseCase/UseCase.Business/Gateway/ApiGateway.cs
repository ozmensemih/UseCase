using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using UseCase.Business.Settings;
using UseCase.Common;
using UseCase.Common.Enums;
using UseCase.DTO;

namespace UseCase.Business.Gateway
{
    public class ApiGateway
    {

        ApiSettings _settings;

        public ApiGateway(ApiSettings settings)
        {
            _settings = settings;
        }

        public ApiResponse<dynamic> AddSubscriptionCustomer(string token, CustomerDto model)
        {
            string  response = GetResponse(_settings.BaseUrl + "Cashier/AddCustomer", HttpMethod.Post, model, token); 
            var result = JsonConvert.DeserializeObject<ApiResponse<dynamic>>(response.ToString());
            return result;
        }

        public ApiResponse<dynamic> AddSubscriptionCorportion(string token, CorporationDto model)
        {
            string response  = GetResponse(_settings.BaseUrl + "Cashier/AddCorporation", HttpMethod.Post, model, token);
            var result = JsonConvert.DeserializeObject<ApiResponse<dynamic>>(response.ToString());
            return result;
        }


        public ApiResponse<CustomerDto> SearchCustomer(string token, SearchCustomerDto model)
        {
            string response = GetResponse(_settings.BaseUrl + "Cashier/SearchCustomer", HttpMethod.Post, model, token);
            var result = JsonConvert.DeserializeObject<ApiResponse<CustomerDto>>(response.ToString());
            return result;
        }

        public ApiResponse<CorporationDto> SearchCorporation(string token, SearchCorporationDto model)
        {
            string response = GetResponse(_settings.BaseUrl + "Cashier/SearchCorporation", HttpMethod.Post, model, token);
            var result = JsonConvert.DeserializeObject<ApiResponse<CorporationDto>>(response.ToString());
            return result;
        }

        public ApiResponse<List<InvoiceDto>> UserInvoiceList(string token, SerachtUserIdInvoiceDto model)
        {
            string response = GetResponse(_settings.BaseUrl + "Invoice/GetUserIdInvoce", HttpMethod.Get, model, token);
            var result = JsonConvert.DeserializeObject<ApiResponse<List<InvoiceDto>>>(response);
            return result;
        }
        public ApiResponse<bool> Paid(string token, Guid id)
        {
            string response = GetResponse(_settings.BaseUrl + "Invoice/Paid", HttpMethod.Post, id, token);
            var result = JsonConvert.DeserializeObject<ApiResponse<bool>>(response);
            return result;
        }

        public ApiResponse<List<InvoiceDto>> UserInvoiceListPaymentStatus(string token, SerachtUserIdInvoiceDto model)
        {
            string response = GetResponse(_settings.BaseUrl + "Invoice/GetUserInvoiceListPaymentStatus", HttpMethod.Get, model, token);
            var result = JsonConvert.DeserializeObject<ApiResponse<List<InvoiceDto>>>(response);
            return result;
        }


        public ApiResponse<bool> CloseSubscription(string token, Guid id)
        {
            string response = GetResponse(_settings.BaseUrl + "Cashier/CloseSubscription", HttpMethod.Post, id, token);
            var result = JsonConvert.DeserializeObject<ApiResponse<bool>>(response);
            return result;
        }

        public ApiResponse<bool> DepositRefund(string token, Guid id)
        {
            string response = GetResponse(_settings.BaseUrl + "Cashier/DepositRefund", HttpMethod.Post,id, token);
            var result = JsonConvert.DeserializeObject<ApiResponse<bool>>(response);
            return result;
        }

        public ApiResponse<LoginResultDto> GetToken(string userName, string password, SubscriptionType type)
        {
            string response = string.Empty;
            var model = new
            {
                Username = userName,
                Password = password
            };
            if (type == SubscriptionType.Customer)
            {
                response = GetResponse(_settings.BaseUrl + "Customer/Login", HttpMethod.Post, model);

            }
            else if (type == SubscriptionType.Corporation)
            {
                response = GetResponse(_settings.BaseUrl + "Corporation/Login", HttpMethod.Post, model);
            }
            var result = JsonConvert.DeserializeObject<ApiResponse<LoginResultDto>>(response);
            return result;
        }

        public ApiResponse<LoginResultDto> LoginCashier(string userName, string password)
        {
            var model = new
            {
                Username = userName,
                Password = password
            };

            string response = GetResponse(_settings.BaseUrl + "Cashier/Login", HttpMethod.Post, model);
            
            var result = JsonConvert.DeserializeObject<ApiResponse<LoginResultDto>>(response);
            return result;
        }



        private string GetResponse(string url, HttpMethod method, object data, string token = null)
        {
            ICredentials credentials = CredentialCache.DefaultNetworkCredentials;
            string message = null;



            var proxyHttpClientHandler = new HttpClientHandler
            {
                DefaultProxyCredentials = credentials,
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; },
                SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
            };

            var client = new HttpClient(proxyHttpClientHandler)
            {
                BaseAddress = new Uri(url)
            };


            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }

            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            string json = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                HttpResponseMessage result = null;
                if (method == HttpMethod.Post)
                {
                    Task<HttpResponseMessage> task = Task.Run(() => client.PostAsync(url, content));
                    result = task.Result;
                }
                else
                {
                    var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    string query = string.Join("&",
                      dictionary.Select(kvp =>
                       string.Format("{0}={1}", kvp.Key, kvp.Value)));
                    var urlAction = string.Format("{0}?{1}", url, query);

                    Task<HttpResponseMessage> task2 = Task.Run(() => client.GetAsync(urlAction));
                    result = task2.Result;
                }

                if (result.StatusCode != HttpStatusCode.OK)
                {

                }

                string responseText = result.Content.ReadAsStringAsync().Result;
                message = responseText;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<dynamic>()
                {
                    IsError = true,
                    Message = ex.Message,
                    Result = null,
                    StatusCode = 500,
                    Type = ResponseMessageEnum.Exception,
                    TraceId = Guid.NewGuid()
                };
                message = JsonConvert.SerializeObject(response);

            }
            return message;
        }
    }
}
