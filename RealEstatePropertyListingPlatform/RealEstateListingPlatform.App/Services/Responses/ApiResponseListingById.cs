using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Services.Responses
{
    public class ApiResponseListingById
    {
        public ListingViewModel Listing { get; set; } = new ListingViewModel();
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; }

    }
}
