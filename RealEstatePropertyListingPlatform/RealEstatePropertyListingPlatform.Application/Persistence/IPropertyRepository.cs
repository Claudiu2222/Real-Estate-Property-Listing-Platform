using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Application.Persistence
{
    public interface IPropertyRepository : IAsyncRepository<Property>
    {
        Task<Result<IReadOnlyList<Property>>> FindByIdOwnerAsync(Guid id);
    }
}
