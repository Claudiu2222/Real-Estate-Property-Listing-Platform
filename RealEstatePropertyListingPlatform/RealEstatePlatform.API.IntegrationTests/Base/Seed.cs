using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;
using RealEstatePropertyListingPlatform.Domain.Records;
using RealEstatePropertyListingPlatform.Infrastructure;

namespace RealEstatePlatform.API.IntegrationTests.Base
{
    public class Seed
    {
        public static Guid ValidPropertyId ;
        public static Guid ValidListingId ;
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

            PriceInfo price1 = new() { Value = 1000, Currency = Currency.RON };
            PriceInfo price2 = new() { Value = 100, Currency = Currency.EUR };
            PriceInfo price3 = new() { Value = 399.99M, Currency = Currency.USD };

            var listings = new List<Listing>
            {

            Listing.Create(properties[0].OwnerId, properties[0].PropertyId,
            "Listing Title Test - 1", price1, "Description Test 1", ["photo-1/1", "photo/2"],
            false).Value,

            Listing.Create(properties[1].OwnerId, properties[1].PropertyId,
            "Listing Title Test - 2", price2, "Description Test 2", ["photo-2/1", "photo/2"],
            false).Value,

            Listing.Create(properties[2].OwnerId, properties[2].PropertyId,
            "Listing Title Test - 3", price3, "Description Test 3", ["photo-3/1", "photo/2"],
            false).Value

            };

            context.Listings.AddRange(listings);
            context.SaveChanges();

            ValidPropertyId = properties[0].PropertyId;
            ValidListingId = listings[0].ListingId;

            var propertyCount = context.Properties.Count();
            Console.WriteLine($"Number of properties in the DbSet: {propertyCount}");


            var listingCount = context.Listings.Count();
            Console.WriteLine($"Number of listings in the DbSet: {listingCount}");

        }
    }
}
