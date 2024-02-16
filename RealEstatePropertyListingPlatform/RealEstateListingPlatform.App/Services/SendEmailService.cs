using RealEstateListingPlatform.App.Contracts;
using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;
using System.Net.Http.Json;

namespace RealEstateListingPlatform.App.Services
{
    public class SendEmailService : ISendMailService
    {
        private const string RequestUri = "api/v1/Email/send";
        private readonly HttpClient _httpClient;

        public SendEmailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponseSendMail> SendMailAsync(SendMailViewModel sendMailViewModel)
        {

            var result = await _httpClient.PostAsJsonAsync(RequestUri, sendMailViewModel);
            var response = await result.Content.ReadFromJsonAsync<ApiResponseSendMail>();
            response!.isSuccess = result.IsSuccessStatusCode;
            return response!;
        }
        
    }
}
