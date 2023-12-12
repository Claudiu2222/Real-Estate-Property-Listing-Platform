using Microsoft.EntityFrameworkCore;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using RealEstatePropertyListingPlatform.Domain.Common;
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

        private readonly ICurrentUserService currentUserService;

        public RealEstatePropertyListingPlatformContext()
        {}
        public RealEstatePropertyListingPlatformContext(DbContextOptions<RealEstatePropertyListingPlatformContext> options, ICurrentUserService currentUserService) : base(options)
        {
            this.currentUserService = currentUserService;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Listing> Listings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Listing>()
                .OwnsOne(listing => listing.Price, price =>
                {
                    price.Property(p => p.Value).HasColumnName("PriceValue");
                    price.Property(p => p.Currency).HasColumnName("PriceCurrency");
                });
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.PhoneNumber).IsUnique();
        }

        /*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                {
                    optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=RealEstatePropertyListingPlatform;UserId=postgres;Password=1234;");
                }*/

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = currentUserService.GetCurrentClaimsPrincipal()?.Claims.FirstOrDefault(c => c.Type == "name")?.Value!;
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedBy = currentUserService.GetCurrentClaimsPrincipal()?.Claims.FirstOrDefault(c => c.Type == "name")?.Value!;
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }



    }
}
