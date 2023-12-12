using Microsoft.EntityFrameworkCore;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Infrastructure.Repositories
{
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(RealEstatePropertyListingPlatformContext context) : base(context)
        {
        }

        //find all properties by owner id
        public async Task<Result<IReadOnlyList<Property>>> FindByIdOwnerAsync(Guid id)
        {
            var result = await context.Set<Property>().Where(x => x.OwnerId == id).ToListAsync();
            if (result == null)
            {
                return Result<IReadOnlyList<Property>>.Failure($"Entity with id {id} not found");
            }
            return Result<IReadOnlyList<Property>>.Success(result);
        }

    }
}
