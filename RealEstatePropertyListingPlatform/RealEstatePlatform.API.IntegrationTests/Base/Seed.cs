using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;
using RealEstatePropertyListingPlatform.Infrastructure;

namespace RealEstatePlatform.API.IntegrationTests.Base
{
    public class Seed
    {
        public static void InitializeDbForTests(RealEstatePropertyListingPlatformContext context)
        {
            
        var properties = new List<Property>
            {
                Property.Create(Guid.NewGuid(), "Maple Street", "New York", "Manhattan", "10001", "USA", PropertyType.Apartment, 2, 1, 1, 3, 1000).Value,
                Property.Create(Guid.NewGuid(), "Oak Avenue", "Los Angeles", "Downtown", "90012", "USA", PropertyType.Apartment, 3, 2, 1, 1, 1500).Value,
                Property.Create(Guid.NewGuid(), "Cherry Lane", "London", "Westminster", "SW1A 1AA", "United Kingdom", PropertyType.Apartment, 1, 1, 3, 5, 800).Value,
                Property.Create(Guid.NewGuid(), "Pine Street", "Sydney", "CBD", "2000", "Australia", PropertyType.Apartment, 2, 2, 5, 10, 1200).Value
            };

            context.Properties.AddRange(properties);
            context.SaveChanges();

            var propertyCount = context.Properties.Count();
            Console.WriteLine($"Number of properties in the DbSet: {propertyCount}");

        }
    }
}
