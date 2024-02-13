using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;

namespace RealEstatePropertyListingPlatform.Domain.Test.PropertyTests
{
    public class PropertyNumberOfRoomsTests : PropertyTests
    {

        private readonly int negativeNumberOfRooms = -1;

        private readonly int invalidNumberOfRoomsInCombinationWithLand = 1;
        private readonly int validNumberOfRoomsInCombinationWithLand = 0;
        private readonly int validFloorInCombinationWithLand = 0;
        private readonly int validNumberOfFloorsInCombinationWithLand = 0;
        private readonly int validNumberOfBathroomsInCombinationWithLand = 0;

        [Fact]
        public void When_CreatePropertyIsCalled_And_NumberOfRoomsIsNegative_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, ValidPropertyType,
                negativeNumberOfRooms, ValidNumberOfBathrooms, ValidFloor, ValidNumberOfFloors, ValidSquareFeet, ValidLongitude, ValidLatitude);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_NumberOfRoomsIsInvalidInCombinationWithLand_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Land,
                invalidNumberOfRoomsInCombinationWithLand, validNumberOfBathroomsInCombinationWithLand,
                validFloorInCombinationWithLand, validNumberOfFloorsInCombinationWithLand,
                ValidSquareFeet, ValidLongitude, ValidLatitude);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_NumberOfRoomsIsValidInCombinationWithLand_Then_SuccessTrue_And_ErrorNull_AndValueNotNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Land,
                validNumberOfRoomsInCombinationWithLand, validNumberOfBathroomsInCombinationWithLand,
                validFloorInCombinationWithLand, validNumberOfFloorsInCombinationWithLand,
                ValidSquareFeet, ValidLongitude, ValidLatitude);
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();
            result.Value.Should().NotBeNull();

        }

    }
}
