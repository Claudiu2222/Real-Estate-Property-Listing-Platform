using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Domain.Test.ListingTests
{
    public class ListingDescriptionTests : ListingTests
    {

        private readonly string nullDescription = null!;
        private readonly string emptyDescription = string.Empty;
        private readonly string tooLongDescription = new('a', 1001);

        [Fact]
        public void When_CreateListingIsCalled_And_DescriptionIsEmpty_Then_SuccessFalse_And_ErrorNotNull_IsReturned()
        {
            // Arrange & Act
            var result = Listing.Create(validListingCreatorId, validPropertyId, validTitle, validPrice,
                emptyDescription, validPhotos, IsRent, validNegotiable);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreateListingIsCalled_And_DescriptionIsNull_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Listing.Create(validListingCreatorId, validPropertyId, validTitle, validPrice,
                nullDescription, validPhotos, IsRent, validNegotiable);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreateListingIsCalled_And_DescriptionIsTooLong_Then_SuccessFalse_And_ErrorNotNull_And_ValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Listing.Create(validListingCreatorId, validPropertyId, validTitle, validPrice,
                tooLongDescription, validPhotos, IsRent, validNegotiable);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

    }
}
