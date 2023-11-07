

using Microsoft.EntityFrameworkCore;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Infrastructure
{
    public class RealEstatePropertyListingPlatformContext : DbContext
    {
        public RealEstatePropertyListingPlatformContext(
            DbContextOptions<RealEstatePropertyListingPlatformContext> options):
            base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Listing> Listings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=GlobalBuyTicketDB;UserId=postgres;Password=postgres;");
        }



    }
}
