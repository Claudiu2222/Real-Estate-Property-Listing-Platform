using RealEstateListingPlatform.App.Contracts;
using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RealEstateListingPlatform.App.Services
{
    public class AzureBlobService : IAzureBlobService
    {
        private const string RequestUri = "api/AzureBlob";
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public AzureBlobService(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public async Task<ApiResponseAzureBlobUpload> UploadFile(AzureBlobModelUploadViewModel azureBlobModelUpload)
        {
            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await _tokenService.GetTokenAsync());

            var result = await _httpClient.PostAsJsonAsync(RequestUri, azureBlobModelUpload);
            var response = await result.Content.ReadFromJsonAsync<ApiResponseAzureBlobUpload>();
            response!.Success = result.IsSuccessStatusCode;
            return response!;
        }


    }
}
