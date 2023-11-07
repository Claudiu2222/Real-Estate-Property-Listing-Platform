using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Entities;

using System.Linq;

using System.Threading.Tasks;

namespace RealEstatePropertyListingPlatform.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(RealEstatePropertyListingPlatformContext context) : base(context)
        {
        }

        //public async Task<Result<User>> FindByEmailAsync(string email)
        //{
        //    var result = await context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();

        //    if (result == null)
        //    {
        //        return Result<User>.Failure($"Entity with email {email} not found");
        //    }
        //    return Result<User>.Success(result);
        //}
    }
    
}
