using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Application.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        public Task<Result<User>> FindByEmailAsync(string email);
        public Task<Result<User>> FindByPhoneNumberAsync(string phoneNumber);
    }
}
