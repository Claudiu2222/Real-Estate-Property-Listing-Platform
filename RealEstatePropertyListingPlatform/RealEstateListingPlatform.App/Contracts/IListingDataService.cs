using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Contracts
{
    public interface IListingDataService
    {
        Task<ApiResponseListingCreate> CreateListingAsync(ListingViewModelCreate listingViewModel);
        Task<List<ListingViewModel>> GetListingsAsync();
        Task<ApiResponseListing> GetPagedListingsAsync(int pageNumber, int pageSize);
    }
}
