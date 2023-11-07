using Microsoft.EntityFrameworkCore;
using RealEstatePropertyListingPlatform.Domain.Entities;
namespace RealEstatePropertyListingPlatform.Infrastructure
{
    public class RealEstatePropertyListingPlatformContext : DbContext
    {
        // public RealEstatePropertyListingPlatformContext(
        //     DbContextOptions<RealEstatePropertyListingPlatformContext> options, DbSet<User> users, DbSet<Property> properties, DbSet<Listing> listings):
        //     base(options)
        // {
        //     Users = users;
        //     Properties = properties;
        //     Listings = listings;
        // }

        public RealEstatePropertyListingPlatformContext()
        {}
        public RealEstatePropertyListingPlatformContext(DbContextOptions<RealEstatePropertyListingPlatformContext> options) : base(options)
        {}

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Listing> Listings { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=DICK;UserId=postgres;Password=123456;");
        }



    }
}
