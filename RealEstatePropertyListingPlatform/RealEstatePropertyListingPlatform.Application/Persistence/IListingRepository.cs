using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Application.Persistence
{
    public interface IListingRepository : IAsyncRepository<Listing>
    {
    }
}
