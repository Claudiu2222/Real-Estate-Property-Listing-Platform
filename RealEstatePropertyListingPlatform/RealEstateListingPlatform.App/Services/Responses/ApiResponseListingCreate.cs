using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Services.Responses
{
    public class ApiResponseListingCreate
    {
        public ListingViewModelCreate Listing { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}
