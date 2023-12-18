using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;

namespace RealEstatePropertyListingPlatform.Domain.Test.PropertyTests
{
    public class PropertyTests
    {
        protected static readonly Guid ValidOwnerId = Guid.NewGuid();
        protected static readonly string ValidStreetName = "StreetName";
        protected static readonly string ValidCity = "City";
        protected static readonly string ValidRegion = "Region";
        protected static readonly string ValidPostalCode = "1000";
        protected static readonly string ValidCountry = "Country";
        protected static readonly PropertyType ValidPropertyType = PropertyType.Apartment;
        protected static readonly int ValidNumberOfRooms = 2;
        protected static readonly int ValidNumberOfBathrooms = 1;
        protected static readonly int ValidFloor = 1;
        protected static readonly int ValidNumberOfFloors = 3;
        protected static readonly int ValidSquareFeet = 100;


        [Fact]
        public void When_CreatePropertyIsCalled_And_AllAreValid_Then_SuccessTrue_And_ErrorNull_And_ValueNotNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, ValidStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, ValidPropertyType,
                ValidNumberOfRooms, ValidNumberOfBathrooms, ValidFloor, ValidNumberOfFloors, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();
            result.Value.Should().NotBeNull();

        }


    }
}
