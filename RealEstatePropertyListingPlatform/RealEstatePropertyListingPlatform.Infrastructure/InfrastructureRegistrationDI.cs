using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstatePropertyListingPlatform.Application.Contracts;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
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

            var apiKey = configuration.GetSection("ExchangeRateApiKey").Value;


            // Register the RealEstatePropertyListingPlatformContext with the API key
            services.AddScoped(provider =>
            {
                var currentUserService = provider.GetRequiredService<ICurrentUserService>();
                var options = provider.GetRequiredService<DbContextOptions<RealEstatePropertyListingPlatformContext>>();
                return new RealEstatePropertyListingPlatformContext(options, currentUserService, apiKey!);
            });

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
            services.AddScoped<IImageStorageService, FirebaseStorageService>();
            return services;

        }
    }
}
