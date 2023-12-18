using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;

namespace RealEstatePropertyListingPlatform.Domain.Test.PropertyTests
{
    public class PropertyNumberOfFloorsTests : PropertyTests
    {
        private readonly int invalidNumberOfFloorsForLandOrFarmOrGarage = 1;
        private readonly int negativeNumberOfFloors = -1;

        private readonly int validFloorForLandOrFarmOrGarageOrHouseOrTownHouseOrVilla = 0;

        private readonly int validNumberOfRoomsForWithLand = 0;
        private readonly int validNumberOfBathroomsForLandOrGarage = 0;

        [Fact]
        public void When_CreatePropertyIsCalled_And_InvalidNumberOfFloorsInCombinationWithLand_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Land,
                validNumberOfRoomsForWithLand, validNumberOfBathroomsForLandOrGarage,
                validFloorForLandOrFarmOrGarageOrHouseOrTownHouseOrVilla, invalidNumberOfFloorsForLandOrFarmOrGarage,
                ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_InvalidNumberOfFloorsInCombinationWithGarage_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Garage,
                ValidNumberOfRooms, validNumberOfBathroomsForLandOrGarage,
                validFloorForLandOrFarmOrGarageOrHouseOrTownHouseOrVilla, invalidNumberOfFloorsForLandOrFarmOrGarage,
                ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_InvalidNumberOfFloorsInCombinationWithFarm_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Farm,
                ValidNumberOfRooms, ValidNumberOfBathrooms,
                validFloorForLandOrFarmOrGarageOrHouseOrTownHouseOrVilla, invalidNumberOfFloorsForLandOrFarmOrGarage,
                ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }



        [Fact]
        public void When_CreatePropertyIsCalled_And_NegativeNumberOfFloorsInCombinationWithApartment_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Apartment,
                ValidNumberOfRooms, ValidNumberOfBathrooms,
                ValidFloor, negativeNumberOfFloors,
                ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_NegativeNumberOfFloorsInCombinationWithHouse_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.House,
                ValidNumberOfRooms, ValidNumberOfBathrooms,
                validFloorForLandOrFarmOrGarageOrHouseOrTownHouseOrVilla, negativeNumberOfFloors,
                ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorLowerThanNrOfFloorsInCombinationWithApartment_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Apartment,
                ValidNumberOfRooms, ValidNumberOfBathrooms,
                ValidFloor+1, ValidFloor,
                ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

    }
}
