using RealEstateListingPlatform.App.Contracts;
using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RealEstateListingPlatform.App.Services
{
    public class PropertyDataService : IPropertyDataService
    {
        private const string RequestUri = "api/v1/properties";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public PropertyDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<PropertyViewModel>> CreatePropertyAsync(PropertyViewModel categoryViewModel)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.PostAsJsonAsync(RequestUri, categoryViewModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<PropertyViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }
    }
}
