using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Domain.Test.ListingTests
{
    public class ListingTests
    {

        protected static readonly Guid validListingCreatorId = Guid.NewGuid();
        protected static readonly Guid validPropertyId = Guid.NewGuid();
        protected static readonly string validTitle = "Title";
        protected static readonly string validDescription = "Description";
        protected static readonly PriceInfo validPrice = new() { Value = 1000, Currency = Currency.RON};
        protected static readonly List<string> validPhotos = ["link/to/Photo1", "link/to/Photo2"];
        protected static readonly bool validNegotiable = true;
        protected static readonly bool IsRent = true;

        [Fact]
        public void When_CreateListingIsCalled_And_AllAreValid_Then_SuccessTrue_And_ErrorNull_And_ValueNotNull_IsReturned()
        {
            // Arrange & Act
            var result = Listing.Create(validListingCreatorId, validPropertyId, validTitle, validPrice, validDescription, validPhotos, IsRent, validNegotiable);
            var price = new PriceInfo { Value = 1000, Currency = Currency.RON };
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();
            result.Value.Should().NotBeNull();

        }


    }
}
