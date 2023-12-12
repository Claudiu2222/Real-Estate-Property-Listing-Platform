using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Services.Responses
{
    public class ApiResponseListing
    {
        public List<ListingViewModel> Listings { get; set; } = new List<ListingViewModel>();
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; }

        public int? TotalCount { get; set; }

        public bool WasFound { get; set; }

    }
}
