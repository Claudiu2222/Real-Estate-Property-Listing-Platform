using RealEstateListingPlatform.App.Contracts;
using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;
using System.Net.Http;
using System.Text.Json;


namespace RealEstateListingPlatform.App.Services
{
    public class ListingDataService : IListingDataService
    {
        private const string RequestUri = "api/v1/Listing";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public ListingDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<List<ListingViewModel>?> GetListingsAsync()
        {
            try
            {
                using (var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead))
                {
                    result.EnsureSuccessStatusCode();
                    var content = await result.Content.ReadAsStringAsync();

                    // Deserialize the root object, which contains the "listings" property
                    var responseObject = JsonSerializer.Deserialize<ResponseObject>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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
                // Handle specific exception or log the error
                // You may also consider logging the response content for debugging
                throw new ApplicationException("Error fetching listings", ex);
            }
        }

        // Create a class to represent the root object of the JSON response
        public class ResponseObject
        {
            public List<ListingViewModel>? Listings { get; set; }
            public bool WasFound { get; set; }
            public bool Success { get; set; }
            public string? Message { get; set; }
            public List<string>? ValidationErrors { get; set; }
        }


    }
}
