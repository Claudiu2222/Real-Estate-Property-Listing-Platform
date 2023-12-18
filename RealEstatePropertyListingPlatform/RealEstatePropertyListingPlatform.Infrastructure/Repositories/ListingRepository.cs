using Microsoft.EntityFrameworkCore;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Infrastructure.Repositories
{
    public class ListingRepository : BaseRepository<Listing>, IListingRepository
    {
        public ListingRepository(RealEstatePropertyListingPlatformContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Listing>> GetPagedListingsByIdAsync(int pageNumber, int pageSize, Guid userId)
        { 
            return await context.Listings
                .Where(x => x.ListingCreatorId == userId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IReadOnlyList<Listing>> GetListingsByUserId(Guid userId)
        {
            return await context.Listings
                .Where(x => x.ListingCreatorId == userId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
