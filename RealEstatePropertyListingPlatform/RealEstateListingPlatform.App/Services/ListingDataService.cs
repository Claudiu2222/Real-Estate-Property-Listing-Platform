using RealEstateListingPlatform.App.Contracts;
using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;


namespace RealEstateListingPlatform.App.Services
{
    public class ListingDataService : IListingDataService
    {
        private const string RequestUri = "api/v1/Listings";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public ListingDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponseListingCreate> CreateListingAsync(ListingViewModelCreate listingViewModel)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.PostAsJsonAsync(RequestUri, listingViewModel);
            var response = await result.Content.ReadFromJsonAsync<ApiResponseListingCreate>();
            response!.Success = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<List<ListingViewModel>> GetListingsAsync()
        {
            try
            {
                using (var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead))
                {
                    result.EnsureSuccessStatusCode();
                    var content = await result.Content.ReadAsStringAsync();

                    // Deserialize the root object, which contains the "listings" property
                    var responseObject = JsonSerializer.Deserialize<ApiResponseListing>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    // Check if "wasFound" is true before accessing "listings"
                    if (responseObject?.WasFound == true)
                    {
                        return responseObject.Listings;
                    }
                    else
                    {
                        // Handle the case when no listings were found
                        return new List<ListingViewModel>();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Error fetching listings", ex);
            }
        }

        public async Task<ListingViewModel> GetListingByIdAsync(Guid id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
                using (var result = await httpClient.GetAsync($"{RequestUri}/{id}", HttpCompletionOption.ResponseHeadersRead))
                {
                    result.EnsureSuccessStatusCode();
                    var content = await result.Content.ReadAsStringAsync();

                    var responseObject = JsonSerializer.Deserialize<ApiResponseListingById>(content,
                                               new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return responseObject.Listing;
                }
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Error fetching listing", ex);
            }
        }
        
        public async Task<ListingViewModel> GetListingByIdAsyncNoAuth(Guid id)
        {
            try
            {
                using var result = await httpClient.GetAsync($"{RequestUri}/{id}", HttpCompletionOption.ResponseHeadersRead);
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();

                var responseObject = JsonSerializer.Deserialize<ApiResponseListingById>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return responseObject!.Listing;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Error fetching listing", ex);
            }
        }

        public async Task<ApiResponseListingById> UpdateListingAsync(ListingViewModel listingViewModel)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.PutAsJsonAsync($"{RequestUri}/{listingViewModel.ListingId}", listingViewModel);
            var response = await result.Content.ReadFromJsonAsync<ApiResponseListingById>();
            response!.Success = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<ApiResponseListingById> DeleteListingAsync(Guid id)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.DeleteAsync($"{RequestUri}/{id}");
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponseListingById>();
            response!.Success = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<ApiResponseListing> GetPagedListingsAsync(int pageNumber, int pageSize)
        {
            try
            {
                using (var result = await httpClient.GetAsync($"{RequestUri}/paginated?page={pageNumber}&size={pageSize}",
                           HttpCompletionOption.ResponseHeadersRead))
                {
                    result.EnsureSuccessStatusCode();
                    var content = await result.Content.ReadAsStringAsync();

                    // Deserialize the root object, which contains the "listings" property
                    var responseObject = JsonSerializer.Deserialize<ApiResponseListing>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    // Check if "wasFound" is true before accessing "listings"
                    return responseObject;

                }
            }
            catch (HttpRequestException ex)
            {
                // Handle specific exception or log the error
                // You may also consider logging the response content for debugging
                throw new ApplicationException("Error fetching listings", ex);
            }
        }

        public async Task<ApiResponseListing> GetPagedListingsForUserAsync(int pageNumber, int pageSize)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
                using (var result = await httpClient.GetAsync(
                           $"{RequestUri}/owner/paginated?page={pageNumber}&size={pageSize}",
                           HttpCompletionOption.ResponseHeadersRead))
                {
                    result.EnsureSuccessStatusCode();
                    var content = await result.Content.ReadAsStringAsync();

                    // Deserialize the root object, which contains the "listings" property
                    var responseObject = JsonSerializer.Deserialize<ApiResponseListing>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    // Check if "wasFound" is true before accessing "listings"
                    return responseObject;

                }
            }
            catch (HttpRequestException ex)
            {
                // Handle specific exception or log the error
                // You may also consider logging the response content for debugging
                throw new ApplicationException("Error fetching listings", ex);
            }
        }
        }




}
