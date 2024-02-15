using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Domain.Test.ListingTests
{
    public class ListingUpdateTests : ListingTests
    {

        private readonly string validUpdatedTitle = "Updated Title";
        private readonly PriceInfo validUpdatedPrice =  new() { Value = 230, Currency = Currency.USD };
        private readonly string validUpdatedDescription = "Updated Description";
        private readonly List<string> validUpdatedPhotos = ["updated/link/to/photo/1", "updated/link/to/photo/2"];
        private readonly bool validUpdatedNegotiable = true;


        [Fact]
        public void When_UpdateListingIsCalled_And_AllAreValid_Then_UpdateIsSuccessfullyMade()
        {
            //Arrange
            Listing listing = Listing.Create(validListingCreatorId, validPropertyId,
            validTitle, validPrice, validDescription, validPhotos,IsRent, validNegotiable).Value;

            //Act
            listing.Update(validUpdatedTitle, validUpdatedPrice, validUpdatedDescription, validUpdatedPhotos, validUpdatedNegotiable);

            //Assert
            listing.Title.Should().Be(validUpdatedTitle);
            listing.Price.Should().Be(validUpdatedPrice);
            listing.Description.Should().Be(validUpdatedDescription);
            listing.Photos.Should().BeEquivalentTo(validUpdatedPhotos);
            listing.Negotiable.Should().Be(validUpdatedNegotiable);
        }

    }
}
