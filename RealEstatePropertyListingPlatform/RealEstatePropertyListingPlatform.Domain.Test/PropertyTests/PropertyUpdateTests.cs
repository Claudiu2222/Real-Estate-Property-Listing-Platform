using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;

namespace RealEstatePropertyListingPlatform.Domain.Test.PropertyTests
{
    public class PropertyUpdateTests : PropertyTests
    {
        private readonly string validStreetNameUpdate = "Valid Street Name Update";
        private readonly string validCityUpdate = "Valid City Update";
        private readonly string validRegionUpdate = "Valid Region Update";
        private readonly string validPostalCodeUpdate = "Valid Postal Code Update";
        private readonly string validCountryUpdate = "Valid Country Update";
        private readonly PropertyType validPropertyTypeUpdate = PropertyType.Condo;
        private readonly int validNumberOfRoomsUpdate = 3;
        private readonly int validNumberOfBathroomsUpdate = 2;
        private readonly int validFloorUpdate = 2;
        private readonly int validNumberOfFloorsUpdate = 4;
        private readonly int validSquareFeetUpdate = 200;
        private readonly string validLongitudeUpdate = "Valid Longitude Update";
        private readonly string validLatitudeUpdate = "Valid Latitude Update";

        [Fact]
        public void When_UpdatePropertyIsCalled_And_AllAreValid_Then_UpdateIsSuccesfullyMade()
        {
            // Arrange

            Property property = Property.Create(
            ValidOwnerId, ValidStreetName, ValidCity, ValidRegion, ValidPostalCode, ValidCountry,
            ValidPropertyType, ValidNumberOfRooms, ValidNumberOfBathrooms, ValidFloor, ValidNumberOfFloors,
            ValidSquareFeet, ValidLongitude, ValidLatitude).Value;

            //Act
            property.Update(validStreetNameUpdate, validCityUpdate, validRegionUpdate,
                            validPostalCodeUpdate, validCountryUpdate, validPropertyTypeUpdate, validNumberOfRoomsUpdate,
                            validNumberOfBathroomsUpdate, validFloorUpdate, validNumberOfFloorsUpdate, validSquareFeetUpdate, validLongitudeUpdate, validLatitudeUpdate);

            //Assert
            property.StreetName.Should().Be(validStreetNameUpdate);
            property.City.Should().Be(validCityUpdate);
            property.Region.Should().Be(validRegionUpdate);
            property.PostalCode.Should().Be(validPostalCodeUpdate);
            property.Country.Should().Be(validCountryUpdate);
            property.PropertyType.Should().Be(validPropertyTypeUpdate);
            property.NumberOfRooms.Should().Be(validNumberOfRoomsUpdate);
            property.NumberOfBathrooms.Should().Be(validNumberOfBathroomsUpdate);
            property.Floor.Should().Be(validFloorUpdate);
            property.NumberOfFloors.Should().Be(validNumberOfFloorsUpdate);
            property.SquareFeet.Should().Be(validSquareFeetUpdate);

  
        }



    }
}
