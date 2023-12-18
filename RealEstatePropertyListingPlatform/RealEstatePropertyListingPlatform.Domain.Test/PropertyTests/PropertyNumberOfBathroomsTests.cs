using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;

namespace RealEstatePropertyListingPlatform.Domain.Test.PropertyTests
{
    public class PropertyNumberOfBathroomsTests : PropertyTests
    {

        private readonly int negativeNumberOfBathrooms = -1;

        private readonly int invalidNumberOfBathroomsInCombinationWithLandOrGarage = 1;
        private readonly int validNumberOfBathroomsInCombinationWithLandOrGarage = 0;
        private readonly int validFloorInCombinationWithLandOrGarage = 0;
        private readonly int validNumberOfFloorsInCombinationWithLandOrGarage = 0;

        private readonly int validNumberOfRoomsInCombinationWithLand = 0;
        private readonly int validNumberOfRoomsInCombinationWithGarage = 1;

        [Fact]
        public void When_CreatePropertyIsCalled_And_NumberOfBathroomsIsNegative_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, ValidPropertyType,
                ValidNumberOfRooms, negativeNumberOfBathrooms, ValidFloor, ValidNumberOfFloors, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_NumberOfBathroomsIsInvalidInCombinationWithLand_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Land,
                validNumberOfRoomsInCombinationWithLand, invalidNumberOfBathroomsInCombinationWithLandOrGarage,
                validFloorInCombinationWithLandOrGarage, validNumberOfFloorsInCombinationWithLandOrGarage,
                ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_NumberOfBathroomsIsInvalidInCombinationWithGarage_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Garage,
                validNumberOfRoomsInCombinationWithGarage, invalidNumberOfBathroomsInCombinationWithLandOrGarage,
                validFloorInCombinationWithLandOrGarage, validNumberOfFloorsInCombinationWithLandOrGarage,
                ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_NumberOfBathroomsIsValidInCombinationWithLand_Then_SuccessTrue_And_ErrorNull_AndValueNotNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Land,
                validNumberOfRoomsInCombinationWithLand, validNumberOfBathroomsInCombinationWithLandOrGarage,
                validFloorInCombinationWithLandOrGarage, validNumberOfFloorsInCombinationWithLandOrGarage,
                ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();
            result.Value.Should().NotBeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_NumberOfBathroomsIsValidInCombinationWithGarage_Then_SuccessTrue_And_ErrorNull_AndValueNotNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Garage,
                validNumberOfRoomsInCombinationWithGarage, validNumberOfBathroomsInCombinationWithLandOrGarage,
                validFloorInCombinationWithLandOrGarage, validNumberOfFloorsInCombinationWithLandOrGarage,
                ValidSquareFeet);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();
            result.Value.Should().NotBeNull();

        }


    }
}
