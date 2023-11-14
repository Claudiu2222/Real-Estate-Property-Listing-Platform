using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Infrastructure.Repositories;

namespace RealEstatePropertyListingPlatform.Infrastructure
{
    public static class InfrastructureRegistrationDI
    {
        public static IServiceCollection AddInfrastructureToDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RealEstatePropertyListingPlatformContext>(
                options => options.UseNpgsql(
                    configuration.GetConnectionString
                    ("RealEstatePropertyConnection"),
                    builder =>
                    builder.MigrationsAssembly(
                        typeof(RealEstatePropertyListingPlatformContext)
                        .Assembly.FullName)));
            services.AddScoped
                (typeof(IAsyncRepository<>),
                typeof(BaseRepository<>));
            services.AddScoped<
                IUserRepository, UserRepository>();
            return services;

        }
    }
}
