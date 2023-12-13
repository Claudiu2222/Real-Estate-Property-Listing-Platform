using Microsoft.EntityFrameworkCore;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Entities;
using System.Xml.Linq;

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

        public async Task<Result<Property>> DeleteAsync(Guid id)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Find the property
                    var property = await context.Set<Property>().FindAsync(id);
                    if (property == null)
                    {
                        return Result<Property>.Failure($"Entity with id {id} not found");
                    }

                    // Delete associated listings
                    var listings = context.Set<Listing>().Where(l => l.PropertyId == id);
                    context.Set<Listing>().RemoveRange(listings);

                    // Delete the property
                    context.Set<Property>().Remove(property);

                    // Save changes and commit the transaction
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Result<Property>.Success(property);
                }
                catch (Exception ex)
                {
                    // Roll back the transaction in case of an error
                    await transaction.RollbackAsync();
                    return Result<Property>.Failure($"Error deleting property: {ex.Message}");
                }
            }
        }

    }
}
