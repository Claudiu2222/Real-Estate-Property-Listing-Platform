using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Application.Persistence
{
    public interface IListingRepository : IAsyncRepository<Listing>
    {
        Task<IReadOnlyList<Listing>> GetPagedListingsByIdAsync(int pageNumber, int pageSize, Guid userId);

        Task<IReadOnlyList<Listing>> GetListingsByUserId(Guid userId);

        Task<IReadOnlyList<Listing>> GetFilteredListingsAsync(decimal priceLowerBound, decimal priceUpperBound, int currency,
                                        string city, string region, int propertyType,
                                        int squareFeetLowerBound, int squareFeetUpperBound,
                                        bool? forRent,
                                        string containsInTitle);

    }
}
