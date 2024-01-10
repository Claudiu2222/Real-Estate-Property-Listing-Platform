using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstatePropertyListingPlatform.Application.Contracts;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Infrastructure.Repositories;
using RealEstatePropertyListingPlatform.Infrastructure.Services;

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
/*            services.AddScoped<
                IUserRepository, UserRepository>(); excluded from project -- Old-users*/
            services.AddScoped<
                IPropertyRepository, PropertyRepository>();
            services.AddScoped<
                IListingRepository, ListingRepository>();
            services.AddScoped
                <IEmailService, EmailService>();
            return services;

        }
    }
}
