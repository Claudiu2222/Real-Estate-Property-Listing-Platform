using RealEstateListingPlatform.App.Contracts;
using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;
using System.Net.Http.Json;

namespace RealEstateListingPlatform.App.Services
{
    public class PredictPriceService : IPredictPriceService
    {
        private const string requestUri = "api/v1/PredictPrice";
        private readonly HttpClient _httpClient;

        public PredictPriceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponsePredictPrice> PredictPriceAsync(PredictPriceViewModel predictPriceViewModel)
        {
            var result = await _httpClient.PostAsJsonAsync(requestUri, predictPriceViewModel);
            var response = await result.Content.ReadFromJsonAsync<ApiResponsePredictPrice>();
            response!.Success = result.IsSuccessStatusCode;
            return response!;
            
        }
    }
}
