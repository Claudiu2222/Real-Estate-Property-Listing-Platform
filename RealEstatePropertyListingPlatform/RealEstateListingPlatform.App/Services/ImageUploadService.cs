using System.Net;
using System.Net.Http.Json;
using RealEstateListingPlatform.App.Contracts;
using RealEstateListingPlatform.App.Services.Responses;

namespace RealEstateListingPlatform.App.Services
{
    public class ImageUploadService : IImageUploadService 
    {
        private string bucket_url = "https://firebasestorage.googleapis.com/v0/b/dot-net-test-20d14.appspot.com";
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;
        private const string RequestUri1 = "api/Images/generate-upload-url";
        private const string RequestUri2 = "api/Images";

        public ImageUploadService(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public async Task<ApiResponseImageUpload> GetPresignedUrl(string imgName)
        {
            
            var response = await _httpClient.GetAsync($"{RequestUri1}?filePath={imgName}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UrlResponse>();
                return new ApiResponseImageUpload
                {
                    Success = true,
                    Message = "Upload URL generated successfully",
                    UploadUrl = result.Url
                };
            }
            return new ApiResponseImageUpload
                {
                    Success = false,
                    Message = "Could not generate the upload URL."
                };
            
        }

        public String GetFileUrl(String imgName)
        {
            return $"{bucket_url}/o/{Uri.EscapeDataString(imgName)}?alt=media";
        }

        public async Task<ApiResponseImageUpload> UploadImageToStorage(String presignedUrl, ByteArrayContent content)
        {
            var response = await _httpClient.PutAsync(presignedUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return new ApiResponseImageUpload
                {
                    Success = true,
                    Message = "Image uploaded successfully"
                };
            }
            return new ApiResponseImageUpload
            {
                Success = false,
                Message = "Could not upload the image."
            };
        }

        public async Task<ApiResponseImageUpload> DeleteImage(String imgName)
        {

            var tok = await _tokenService.GetTokenAsync();
            Console.WriteLine(tok);
            _httpClient.DefaultRequestHeaders.Authorization
                = new("Bearer", tok);
            
            var response = await _httpClient.DeleteAsync($"{RequestUri2}/{imgName}");
            if (response.IsSuccessStatusCode)
            {
                return new ApiResponseImageUpload
                {
                    Success = true,
                    Message = "Image deleted successfully"
                };
            }
            return new ApiResponseImageUpload
            {
                Success = false,
                Message = "Could not delete the image."
            };
        }
    }
}
