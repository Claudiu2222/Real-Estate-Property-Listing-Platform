using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Application.Persistence
{
    public interface IPropertyRepository : IAsyncRepository<Property>
    {
    }
}
