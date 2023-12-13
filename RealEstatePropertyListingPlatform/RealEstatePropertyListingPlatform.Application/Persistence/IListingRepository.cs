using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Application.Persistence
{
    public interface IListingRepository : IAsyncRepository<Listing>
    {
        Task<IReadOnlyList<Listing>> GetPagedListingsByIdAsync(int pageNumber, int pageSize, Guid userId);

        Task<IReadOnlyList<Listing>> GetListingsByUserId(Guid userId);
    }
}
