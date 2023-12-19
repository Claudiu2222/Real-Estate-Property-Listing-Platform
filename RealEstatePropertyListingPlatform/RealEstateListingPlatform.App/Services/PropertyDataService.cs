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
        private const string RequestBasicInfoUri = "api/v1/properties/basicinfo";
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

        public async Task<ApiResponsePropertyById> UpdatePropertyAsync(PropertyViewModelByUser categoryViewModel, Guid id)
        {
        
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.PutAsJsonAsync($"{RequestUri}/{id}", categoryViewModel);
            var response = new ApiResponsePropertyById();
            /*try
            {
                result.EnsureSuccessStatusCode();
                response = await result.Content.ReadFromJsonAsync<ApiResponse<PropertyViewModelByUser>>();
                response!.IsSuccess = result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                response!.IsSuccess = false;
            }*/
            response = await result.Content.ReadFromJsonAsync<ApiResponsePropertyById>();
                response!.Success = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<PropertyViewModelByUser> GetPropertyByIdAsync(Guid id)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.GetAsync($"{RequestUri}/{id}");
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            var response = JsonSerializer.Deserialize<ApiResponsePropertyById>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            //it is only one property
            Console.WriteLine($"PropertyId: {response!.Property.PropertyId}, StreetName: {response!.Property.StreetName}, City: {response!.Property.City}");
            return response!.Property;
        }
        
        public async Task<PropertyViewModelByUser> GetBasicInfoPropertyByIdAsync(Guid id)
        {
            try
            {
                using var result = await httpClient.GetAsync($"{RequestBasicInfoUri}/{id}", HttpCompletionOption.ResponseHeadersRead);
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();

                var responseObject = JsonSerializer.Deserialize<ApiResponsePropertyById>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return responseObject!.Property;
            } catch (HttpRequestException ex)
            {
                throw new ApplicationException("Error fetching property", ex);
            }
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
