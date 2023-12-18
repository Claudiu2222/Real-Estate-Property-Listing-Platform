using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Domain.Test.PropertyTests
{
    public class PropertyCityTests : PropertyTests
    {
        private readonly string nullCity = null!;
        private readonly string emptyCity = string.Empty;
        private readonly string tooLongCity = new string('a', 101);

        [Fact]
        public void When_CreatePropertyIsCalled_And_CityIsEmpty_Then_SuccessFalse_And_ErrorNotNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                emptyCity, ValidRegion, ValidPostalCode, ValidCountry, ValidPropertyType,
                ValidNumberOfRooms, ValidNumberOfBathrooms, ValidFloor, ValidNumberOfFloors, ValidSquareFeet);
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
                nullCity, ValidRegion, ValidPostalCode, ValidCountry, ValidPropertyType,
                ValidNumberOfRooms, ValidNumberOfBathrooms, ValidFloor, ValidNumberOfFloors, ValidSquareFeet);
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
                tooLongCity, ValidRegion, ValidPostalCode, ValidCountry, ValidPropertyType,
                ValidNumberOfRooms, ValidNumberOfBathrooms, ValidFloor, ValidNumberOfFloors, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }


    }
}
