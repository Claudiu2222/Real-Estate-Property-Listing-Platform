using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;
using RealEstatePropertyListingPlatform.Domain.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePropertyListingPlatform.Domain.Test.ListingTests
{
    public class ListingPriceInfoTests : ListingTests
    {

        private readonly PriceInfo negativeValuePrice = new() {Value = -1, Currency = Currency.USD};
        private readonly PriceInfo invalidCurrencyPrice = new() {Value = 100, Currency = (Currency)100};
        private readonly PriceInfo invalidPriceInfo = new() { Value = -100.0m, Currency = (Currency)101};

        [Fact]
        public void When_CreateListingIsCalled_And_PriceInfoValueIsNegative_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Listing.Create(validListingCreatorId, validPropertyId, validTitle, negativeValuePrice,
                validDescription, validPhotos, validNegotiable);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }


        [Fact]
        public void When_CreateListingIsCalled_And_PriceInfoCurrencyIsInvalid_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Listing.Create(validListingCreatorId, validPropertyId, validTitle, invalidCurrencyPrice,
                validDescription, validPhotos, validNegotiable);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreateListingIsCalled_And_PriceInfoIsInvalid_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Listing.Create(validListingCreatorId, validPropertyId, validTitle, invalidPriceInfo,
                validDescription, validPhotos, validNegotiable);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }


    }
}
