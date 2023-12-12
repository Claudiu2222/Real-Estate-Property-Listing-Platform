using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Services.Responses
{
    public class ApiResponseProperty
    {
            public List<PropertyViewModelByUser> Properties { get; set; } = new List<PropertyViewModelByUser>();
            public bool Success { get; set; }
            public string Message { get; set; }
            public List<string> ValidationErrors { get; set; }
    }
}
