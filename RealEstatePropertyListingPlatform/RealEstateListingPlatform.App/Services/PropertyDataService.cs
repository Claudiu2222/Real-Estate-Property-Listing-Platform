using RealEstateListingPlatform.App.Contracts;
using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealEstateListingPlatform.App.Services
{
    public class PropertyDataService : IPropertyDataService
    {
        private const string RequestUri = "api/v1/properties";
        private const string RequestUriByOwner = "api/v1/properties/owner";
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

        public async Task<ApiResponse<PropertyViewModel>> DeletePropertyAsync(Guid id)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.DeleteAsync($"{RequestUri}/{id}");
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<PropertyViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        

        public async Task<List<PropertyViewModelByUser>> GetAllPropertiesByOwnerAsync()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            var response = JsonSerializer.Deserialize<ApiResponseProperty>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            foreach (var property in response!.Properties)
            {
                Console.WriteLine($"PropertyId: {property.PropertyId}, StreetName: {property.StreetName}, City: {property.City}");
            }

            return response!.Properties;
        }


    }
}
