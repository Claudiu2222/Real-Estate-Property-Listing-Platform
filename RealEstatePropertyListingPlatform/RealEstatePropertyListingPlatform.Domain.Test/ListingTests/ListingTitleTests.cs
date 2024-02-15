using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Domain.Test.ListingTests
{
    public class ListingTitleTests : ListingTests
    {
        private readonly string nullTitle = null!;
        private readonly string emptyTitle = string.Empty;
        private readonly string tooLongTitle = new string('a', 101);

        [Fact]
        public void When_CreatePropertyIsCalled_And_TitleIsEmpty_Then_SuccessFalse_And_ErrorNotNull_IsReturned()
        {
            // Arrange & Act
            var result = Listing.Create(validListingCreatorId, validPropertyId, emptyTitle, validPrice,
                validDescription, validPhotos, IsRent, validNegotiable);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_TitleIsNull_Then_SuccessFalse_And_ErrorNotNull_IsReturned()
        {
            // Arrange & Act
            var result = Listing.Create(validListingCreatorId, validPropertyId, nullTitle, validPrice,
                validDescription, validPhotos, IsRent, validNegotiable);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_TitleIsTooLong_Then_SuccessFalse_And_ErrorNotNull_IsReturned()
        {
            // Arrange & Act
            var result = Listing.Create(validListingCreatorId, validPropertyId, tooLongTitle, validPrice,
                validDescription, validPhotos, IsRent, validNegotiable);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }


    }
}
