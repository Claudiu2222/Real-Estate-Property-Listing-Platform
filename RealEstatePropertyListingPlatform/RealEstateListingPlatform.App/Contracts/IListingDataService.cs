using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Contracts
{
    public interface IListingDataService
    {
        Task<List<ListingViewModel>> GetListingsAsync();
        Task<ApiResponseListing> GetPagedListingsAsync(int pageNumber, int pageSize);
    }
}
