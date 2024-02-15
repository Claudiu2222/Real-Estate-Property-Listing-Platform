using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Domain.Test.PropertyTests
{

    public class PropertyPostalCodeTests : PropertyTests
    {

        private readonly string nullPostalCode = null!;
        private readonly string emptyPostalCode = string.Empty;
        private readonly string tooLongPostalCode = new ('a', 101);

        [Fact]
        public void When_CreatePropertyIsCalled_And_PostalCodeIsEmpty_Then_SuccessFalse_And_ErrorNotNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, emptyPostalCode, ValidCountry, ValidPropertyType,
                ValidNumberOfRooms, ValidNumberOfBathrooms, ValidFloor, ValidNumberOfFloors, ValidSquareFeet, ValidPostalCode, ValidCountry);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_PostalCodeIsNull_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, nullPostalCode, ValidCountry, ValidPropertyType,
                ValidNumberOfRooms, ValidNumberOfBathrooms, ValidFloor, ValidNumberOfFloors, ValidSquareFeet, ValidLongitude, ValidLatitude);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_PostalCodeIsTooLong_Then_SuccessFalse_And_ErrorNotNull_And_ValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, tooLongPostalCode, ValidCountry, ValidPropertyType,
                ValidNumberOfRooms, ValidNumberOfBathrooms, ValidFloor, ValidNumberOfFloors, ValidSquareFeet, ValidLongitude, ValidLatitude);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

    }
}
