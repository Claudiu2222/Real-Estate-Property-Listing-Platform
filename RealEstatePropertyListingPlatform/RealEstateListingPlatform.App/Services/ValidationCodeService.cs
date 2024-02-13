using RealEstateListingPlatform.App.Contracts;
using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;
using System.Net.Http.Json;

namespace RealEstateListingPlatform.App.Services
{
    public class ValidationCodeService : IValidationCodeService
    {
        private const string RequestUri = "api/v1/Users/insertvalidationcode";
        private const string RequestUri2 = "api/v1/Users/validatecode";
        private const string RequestUri3 = "api/v1/Users/deletevalidationcode";
        private readonly HttpClient _httpClient;

        public ValidationCodeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponseValidationCode> SendValidationCodeAsync(ValidationCodeViewModel email)
        {
            var result = await _httpClient.PutAsync($"{RequestUri}?email={email.Email}", null);
            var response = await result.Content.ReadFromJsonAsync<ApiResponseValidationCode>();
            response!.isSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<ApiResponseValidationCode> ValidateValidationCodeAsync(ValidateValidationCodeViewModel validateValidationCodeViewModel)
        {
            var result = await _httpClient.GetAsync($"{RequestUri2}?email={validateValidationCodeViewModel.Email}&code={validateValidationCodeViewModel.Code}");
            var response = await result.Content.ReadFromJsonAsync<ApiResponseValidationCode>();
            response!.isSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<ApiResponseValidationCode> DeleteValidationCodeAsync(ValidationCodeViewModel email)
        {
            var result = await _httpClient.DeleteAsync($"{RequestUri3}?email={email.Email}");
            var response = await result.Content.ReadFromJsonAsync<ApiResponseValidationCode>();
            response!.isSuccess = result.IsSuccessStatusCode;
            return response!;
        }

    }
}
