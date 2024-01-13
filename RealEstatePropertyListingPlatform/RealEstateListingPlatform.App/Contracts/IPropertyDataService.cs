using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Contracts
{
    public interface IPropertyDataService
    {

        Task<ApiResponsePropertyById> CreatePropertyAsync(PropertyViewModel categoryViewModel);

        Task<ApiResponse<PropertyViewModel>> DeletePropertyAsync(Guid id);

        Task<ApiResponsePropertyById> UpdatePropertyAsync(PropertyViewModelByUser categoryViewModel, Guid id);

        Task<PropertyViewModelByUser> GetPropertyByIdAsync(Guid id);

        Task<List<PropertyViewModelByUser>> GetAllPropertiesByOwnerAsync();
        
        Task<PropertyViewModelByUser> GetBasicInfoPropertyByIdAsync(Guid id);
    }
}
