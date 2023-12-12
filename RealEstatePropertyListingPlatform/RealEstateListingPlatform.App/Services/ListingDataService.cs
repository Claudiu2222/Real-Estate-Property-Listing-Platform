using RealEstateListingPlatform.App.Contracts;
using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;
using System.Net.Http;
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
        }




}
