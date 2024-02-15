using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Domain.Test.PropertyTests
{
    public class PropertyRegionTests : PropertyTests
    {
        private readonly string nullRegion = null!;
        private readonly string emptyRegion = string.Empty;
        private readonly string tooLongRegion = new string('a', 101);

        [Fact]
        public void When_CreatePropertyIsCalled_And_RegionIsEmpty_Then_SuccessFalse_And_ErrorNotNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, emptyRegion, ValidPostalCode, ValidCountry, ValidPropertyType,
                ValidNumberOfRooms, ValidNumberOfBathrooms, ValidFloor, ValidNumberOfFloors, ValidSquareFeet, ValidLongitude, ValidLatitude);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_CityIsNull_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, nullRegion, ValidPostalCode, ValidCountry, ValidPropertyType,
                ValidNumberOfRooms, ValidNumberOfBathrooms, ValidFloor, ValidNumberOfFloors, ValidSquareFeet, ValidLongitude, ValidLatitude);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_CityIsTooLong_Then_SuccessFalse_And_ErrorNotNull_And_ValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, tooLongRegion, ValidPostalCode, ValidCountry, ValidPropertyType,
                ValidNumberOfRooms, ValidNumberOfBathrooms, ValidFloor, ValidNumberOfFloors, ValidSquareFeet, ValidLongitude, ValidLatitude);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }
    }
}
