using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Contracts
{
    public interface IListingDataService
    {
        Task<ApiResponseListingCreate> CreateListingAsync(ListingViewModelCreate listingViewModel);
        Task<List<ListingViewModel>> GetListingsAsync();
        Task<ListingViewModel> GetListingByIdAsync(Guid id);
        Task<ApiResponseListingById> UpdateListingAsync(ListingViewModel listingViewModel);
        Task<ApiResponseListingById> DeleteListingAsync(Guid id);
        Task<ApiResponseListing> GetPagedListingsAsync(int pageNumber, int pageSize);
        Task<ApiResponseListing> GetPagedListingsForUserAsync(int pageNumber, int pageSize);
        Task<ListingViewModel> GetListingByIdAsyncNoAuth(Guid id);
    }
}
