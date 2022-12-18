using Newtonsoft.Json;
using SiteManagement.Domain.PaymentApiModel;
using SiteManagement.Infrastructure.IServices.APIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Application.Services
{
    /// <summary>
    /// Paymetapi ye baglanilan kisim
    /// </summary>
    public class PaymentAPIService : IPaymentAPIService
    {
        private readonly HttpClient _httpClient;
        public PaymentAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ApiResponse<string>> CreatePayment(CreatePaymentDto createPaymentDto)
        {
            const string requestUri = "https://localhost:5001/api/Payment/CreatePayment";

            string requestJson = JsonConvert.SerializeObject(createPaymentDto);

            HttpResponseMessage httpResponse;
            using (var stringcontent = new StringContent(requestJson, Encoding.UTF8, "application/json"))
            {
                httpResponse = await _httpClient.PostAsync(requestUri, stringcontent);
                var apiResponse = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApiResponse<string>>(apiResponse);
            }
        }
    }
}
