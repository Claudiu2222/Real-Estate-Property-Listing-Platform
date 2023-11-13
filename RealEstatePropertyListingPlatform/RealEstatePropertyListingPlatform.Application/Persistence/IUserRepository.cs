using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Application.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}
