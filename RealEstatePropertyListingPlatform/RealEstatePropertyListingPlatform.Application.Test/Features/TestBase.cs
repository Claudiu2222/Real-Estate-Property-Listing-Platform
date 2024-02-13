using NSubstitute;
using RealEstatePropertyListingPlatform.Application.Contracts;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Application.Test.Features
{
    public class TestBase : IDisposable
    {
        protected IListingRepository ListingRepository { get; }
        protected IPropertyRepository PropertyRepository { get; }

        protected ICurrentUserService CurrentUserService { get; }
        protected IImageStorageService ImageStorageService { get; }

        protected Property ValidProperty { get; }

        protected Listing ValidListing1 { get; }

        protected Listing ValidListing2 { get; }

        public TestBase()
        {
            ListingRepository = Substitute.For<IListingRepository>();
            PropertyRepository = Substitute.For<IPropertyRepository>();
            CurrentUserService = Substitute.For<ICurrentUserService>();
            ImageStorageService = Substitute.For<IImageStorageService>();

            ValidProperty = Property.Create(Guid.NewGuid(), "Test Address", "Test Zip Code", "Test State",
                               "Test Country", "Romania", Domain.Enums.PropertyType.Apartment, 2, 2, 2, 2, 2).Value;
            ValidListing1 = Listing.Create(ValidProperty.OwnerId, ValidProperty.PropertyId, "Test listing 1",
                               new Domain.Records.PriceInfo { Value = 100, Currency = Domain.Enums.Currency.USD }, "Test Description",
                                              new List<string> { "Test Photo" }, true).Value;
            ValidListing2 = Listing.Create(ValidProperty.OwnerId, ValidProperty.PropertyId, "Test listing 2", 
                               new Domain.Records.PriceInfo { Value = 100, Currency = Domain.Enums.Currency.USD }, "Test Description",
                                              new List<string> { "Test Photo" }, true).Value;
        }

        public void Dispose()
        {
            ListingRepository.ClearReceivedCalls();
            PropertyRepository.ClearReceivedCalls();
            CurrentUserService.ClearReceivedCalls();
            ImageStorageService.ClearReceivedCalls();
        }
    }
    
}
