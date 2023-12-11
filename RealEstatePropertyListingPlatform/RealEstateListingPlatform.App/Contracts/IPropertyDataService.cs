using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Contracts
{
    public interface IPropertyDataService
    {

        Task<ApiResponse<PropertyViewModel>> CreatePropertyAsync(PropertyViewModel categoryViewModel);

    }
}
