using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;

namespace RealEstatePropertyListingPlatform.Domain.Test.PropertyTests
{
    public class PropertyFloorTests : PropertyTests
    {

        private readonly int negativeFloor = -1;
        private readonly int positiveFloor = 1;
        private readonly int validFloorForLandOrFarmOrGarageOrHouseOrTownHouseOrVilla = 0;

        private readonly int validNumberOfFloorsForLandOrFarmOrGarage = 0;
        private readonly int validNumberOfFloorsForVillaOrHouseOrTownhouse = 1;

        private readonly int validNumberOfRoomsForWithLand = 0;
        private readonly int validNumberOfBathroomsForLandOrGarage = 0;


        // For Land, Farm and Garage, Floor must be 0


        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorIsNegativeInCombinationWithLand_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Land,
                validNumberOfRoomsForWithLand, validNumberOfBathroomsForLandOrGarage,
                negativeFloor, validNumberOfFloorsForLandOrFarmOrGarage,
                ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorIsNegativeInCombinationWithFarm_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Farm,
                ValidNumberOfRooms, ValidNumberOfBathrooms, negativeFloor, validNumberOfFloorsForLandOrFarmOrGarage, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorIsNegativeInCombinationWithGarage_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Garage,
                ValidNumberOfRooms, validNumberOfBathroomsForLandOrGarage,
                negativeFloor, validNumberOfFloorsForLandOrFarmOrGarage,
                ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorIsPositiveInCombinationWithLand_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Land,
                validNumberOfRoomsForWithLand, validNumberOfBathroomsForLandOrGarage,
                positiveFloor, validNumberOfFloorsForLandOrFarmOrGarage,
                ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorIsPositiveInCombinationWithFarm_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Farm,
                ValidNumberOfRooms, ValidNumberOfBathrooms, positiveFloor, validNumberOfFloorsForLandOrFarmOrGarage, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorIsPositiveInCombinationWithGarage_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Garage,
                ValidNumberOfRooms, validNumberOfBathroomsForLandOrGarage,
                positiveFloor, validNumberOfFloorsForLandOrFarmOrGarage,
                ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }



        // For Villa, House and Townhouse, Floor must be also 0 (you cannot sell only a floor of a house)
        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorIsNegativeInCombinationWithVilla_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Villa,
                ValidNumberOfRooms, ValidNumberOfBathrooms, negativeFloor, validNumberOfFloorsForVillaOrHouseOrTownhouse, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorIsNegativeInCombinationWithHouse_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.House,
                ValidNumberOfRooms, ValidNumberOfBathrooms,
                negativeFloor, validNumberOfFloorsForVillaOrHouseOrTownhouse,
                ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorIsNegativeInCombinationWithTownhouse_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Townhouse,
                ValidNumberOfRooms, ValidNumberOfBathrooms, negativeFloor, validNumberOfFloorsForVillaOrHouseOrTownhouse, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorIsPositiveInCombinationWithVilla_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Villa,
                ValidNumberOfRooms, ValidNumberOfBathrooms, positiveFloor, validNumberOfFloorsForVillaOrHouseOrTownhouse, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorIsPositiveInCombinationWithHouse_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.House,
                ValidNumberOfRooms, ValidNumberOfBathrooms,
                positiveFloor, validNumberOfFloorsForVillaOrHouseOrTownhouse,
                ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorIsPositiveInCombinationWithTownhouse_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Townhouse,
                ValidNumberOfRooms, ValidNumberOfBathrooms, positiveFloor, validNumberOfFloorsForVillaOrHouseOrTownhouse, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorIsValidInCombinationWithTownhouse_Then_SuccessTrue_And_ErrorNull_AndValueNotNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Townhouse,
                ValidNumberOfRooms, ValidNumberOfBathrooms, validFloorForLandOrFarmOrGarageOrHouseOrTownHouseOrVilla,
                validNumberOfFloorsForVillaOrHouseOrTownhouse, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();
            result.Value.Should().NotBeNull();

        }



        // For Apartment, Condo and Office, Floor must be positive
        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorIsNegativeInCombinationWithApartment_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Apartment,
                ValidNumberOfRooms, ValidNumberOfBathrooms, negativeFloor, ValidNumberOfFloors, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorIsNegativeInCombinationWithCondo_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Condo,
                ValidNumberOfRooms, ValidNumberOfBathrooms, negativeFloor, ValidNumberOfFloors, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_FloorIsNegativeInCombinationWithOffice_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, PropertyType.Office,
                ValidNumberOfRooms, ValidNumberOfBathrooms, negativeFloor, ValidNumberOfFloors, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

    }
}
