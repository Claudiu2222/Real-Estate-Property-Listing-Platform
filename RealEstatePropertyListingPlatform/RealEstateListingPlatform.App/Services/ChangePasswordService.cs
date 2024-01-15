using RealEstateListingPlatform.App.Contracts;
using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RealEstateListingPlatform.App.Services
{
    public class ChangePasswordService : IChangePasswordService
    {
        private const string RequestUri = "api/v1/Users/changepassword";
        private const string RequestUri2 = "api/v1/Users/resetpasswordnotconnected";
        private const string RequestUri3 = "api/v1/Users/isvalidemail";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public ChangePasswordService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponseChangePassword> ChangePasswordAsync(ChangePasswordViewModel changePasswordViewModel)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.PutAsJsonAsync(RequestUri, changePasswordViewModel);
            var response = await result.Content.ReadFromJsonAsync<ApiResponseChangePassword>();
            response!.isSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<ApiResponseChangePassword> ChangePasswordNoConnectionAsync(ChangePasswordNoConnectionViewModel resetPasswordNotConnectedViewModel)
        {
            var result = await httpClient.PutAsJsonAsync(RequestUri2, resetPasswordNotConnectedViewModel);
            var response = await result.Content.ReadFromJsonAsync<ApiResponseChangePassword>();
            response!.isSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<ApiResponseChangePassword> IsValidMail(IsValidMailViewModel isValidMailViewModel)
        {
            var result = await httpClient.GetAsync($"{RequestUri3}?email={isValidMailViewModel.Email}");
            var response = await result.Content.ReadFromJsonAsync<ApiResponseChangePassword>();
            response!.isSuccess = result.IsSuccessStatusCode;
            return response!;
        }
    }
}
