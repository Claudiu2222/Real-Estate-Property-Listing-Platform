using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Domain.Test.ListingTests
{
    public class ListingPhotosTests : ListingTests
    {

        private readonly List<string> emptyListPhoto = [];
        private readonly List<string> emptyLinkInListPhoto1 = [" ", "link/to/photo2" ];
        private readonly List<string> emptyLinkInListPhoto2 = ["link/to/photo1", null! ];

        [Fact]
        public void When_CreateListingIsCalled_And_ListingPhotosIsEmpty_Then_SuccessFalse_And_ErrorNotNull_IsReturned()
        {
            // Arrange & Act
            var result = Listing.Create(validListingCreatorId, validPropertyId, validTitle, validPrice,
                validDescription, emptyListPhoto,IsRent, validNegotiable);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreateListingIsCalled_And_ListingPhotosHasEmptyLink_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Listing.Create(validListingCreatorId, validPropertyId, validTitle, validPrice,
                validDescription, emptyLinkInListPhoto1, IsRent, validNegotiable);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreateListingIsCalled_And_ListingPhotosHasNullLink_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Listing.Create(validListingCreatorId, validPropertyId, validTitle, validPrice,
                validDescription, emptyLinkInListPhoto2, IsRent, validNegotiable);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }



    }
}
