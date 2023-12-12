using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Services.Responses
{
    public class ApiResponsePropertyById
    {
        public PropertyViewModelByUser Property { get; set; } = new PropertyViewModelByUser();
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}
