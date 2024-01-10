using RealEstateListingPlatform.App.Contracts;
using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RealEstateListingPlatform.App.Services
{
    public class SendEmailService : ISendMailService
    {
        private const string RequestUri = "api/v1/Email/send";
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public SendEmailService(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public async Task<ApiResponseSendMail> SendMailAsync(SendMailViewModel sendMailViewModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await _tokenService.GetTokenAsync());

            var result = await _httpClient.PostAsJsonAsync(RequestUri, sendMailViewModel);
            var response = await result.Content.ReadFromJsonAsync<ApiResponseSendMail>();
            response!.isSuccess = result.IsSuccessStatusCode;
            return response!;
        }
        
    }
}
