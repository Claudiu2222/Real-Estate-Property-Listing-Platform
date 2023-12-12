using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Contracts
{
    public interface IListingDataService
    {
        Task<List<ListingViewModel>> GetListingsAsync();
    }
}
