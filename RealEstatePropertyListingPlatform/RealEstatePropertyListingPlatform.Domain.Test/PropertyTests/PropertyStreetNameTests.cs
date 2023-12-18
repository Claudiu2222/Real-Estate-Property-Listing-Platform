using FluentAssertions;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Domain.Test.PropertyTests
{
    public class PropertyStreetNameTests : PropertyTests
    {
        private readonly string nullStreetName = null!;
        private readonly string emptyStreetName = string.Empty;
        private readonly string tooLongStreetName = new string('a', 101);

        [Fact]
        public void When_CreatePropertyIsCalled_And_StreetNameIsEmpty_Then_SuccessFalse_And_ErrorNotNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, emptyStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, ValidPropertyType,
                ValidNumberOfRooms, ValidNumberOfBathrooms, ValidFloor, ValidNumberOfFloors, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_StreetNameIsNull_Then_SuccessFalse_And_ErrorNotNull_AndValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, nullStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, ValidPropertyType,
                ValidNumberOfRooms, ValidNumberOfBathrooms, ValidFloor, ValidNumberOfFloors, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

        [Fact]
        public void When_CreatePropertyIsCalled_And_StreetNameIsTooLong_Then_SuccessFalse_And_ErrorNotNull_And_ValueNull_IsReturned()
        {
            // Arrange & Act
            var result = Property.Create(ValidOwnerId, tooLongStreetName,
                ValidCity, ValidRegion, ValidPostalCode, ValidCountry, ValidPropertyType,
                ValidNumberOfRooms, ValidNumberOfBathrooms, ValidFloor, ValidNumberOfFloors, ValidSquareFeet);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Value.Should().BeNull();

        }

    }
}