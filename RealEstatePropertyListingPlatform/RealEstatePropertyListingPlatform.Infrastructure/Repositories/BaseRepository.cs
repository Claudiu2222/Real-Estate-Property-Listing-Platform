
using Microsoft.EntityFrameworkCore;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Common;

namespace RealEstatePropertyListingPlatform.Infrastructure.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly RealEstatePropertyListingPlatformContext context;

        public BaseRepository(RealEstatePropertyListingPlatformContext context)
        {
            this.context = context;
        }
        public virtual async Task<Result<T>> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity); 
            await context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }

        public virtual async Task<Result<T>> DeleteAsync(Guid id)
        {
            var result = await FindByIdAsync(id);
            if (result != null)
            {
                context.Set<T>().Remove(result.Value);
                await context.SaveChangesAsync();
                return Result<T>.Success(result.Value);
            }
            return Result<T>.Failure($"Entity with id {id} not found");
        }

        public virtual async Task<Result<T>> FindByIdAsync(Guid id)
        {
            var result = await context.Set<T>().FindAsync(id);
            if (result == null)
            {
                return Result<T>.Failure($"Entity with id {id} not found");
            }
            return Result<T>.Success(result);
        }

        public virtual async Task<Result<IReadOnlyList<T>>> GetPagedReponseAsync(int page, int size)
        {
            var result = await context.Set<T>().Skip((page-1)*size).Take(size).AsNoTracking().ToListAsync();
        /*          if (result == null)
            {
                return Result<IReadOnlyList<T>>.Failure($"Failed to retrieve entities at page {page} with size {size}");
            }*/
            return Result<IReadOnlyList<T>>.Success(result);
        }

        public virtual async Task<Result<T>> UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }

        public virtual async Task<Result<IReadOnlyList<T>>> GetAllAsync()
        {
            var result = await context.Set<T>().ToListAsync();
            return Result<IReadOnlyList<T>>.Success(result);
        }

        public virtual async Task<Result<int[]>> GetCountAsync()
        {
            var result = await context.Set<T>().CountAsync();
            return Result<int[]>.Success(new int[]{result});
        }
    }
}
