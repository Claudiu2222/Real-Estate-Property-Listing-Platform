using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;

namespace RealEstatePropertyListingPlatform.Domain.Test.PropertyTests
{
    public class PropertyPropertyTypeTests : PropertyTests
    {

        private readonly PropertyType invalidPropertyType = (PropertyType)10;

        [Fact]
        public void When_CreatePropertyIsCalled_And_PropertyTypeIsInvalid_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, invalidPropertyType,
                ValidNumberOfRooms, ValidNumberOfBathrooms, ValidFloor, ValidNumberOfFloors, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

    }
}
