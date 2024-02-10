using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RealEstatePlatform.API.IntegrationTests.Base;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.CreateListing;
using RealEstatePropertyListingPlatform.Application.Features.Properties;
using RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.CreateProperty;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace RealEstatePlatform.API.IntegrationTests.Controllers
{
    public class PropertiesControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "/api/v1/Properties";

        private static string streetName = "Test Street";
        private static string city = "Test City";
        private static string region = "Test Region";
        private static string postalCode = "Test Postal Code";
        private static string country = "Test Country";
        private static PropertyType propertyType = PropertyType.Apartment;
        private static int numberOfRooms = 2;
        private static int numberOfBathrooms = 1;
        private static int floor = 1;
        private static int numberOfFloors = 3;
        private static int squareFeet = 1000;
        private static string Longitude = "Test Longitude";
        private static string Latitude = "Test Latitude";


        [Fact]
        public async Task When_GetAllPropertiesQueryHandlerIsCalled_Then_Success()
        {
            // Arrange && Act
            var response = await Client.GetAsync(RequestUri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(responseString);

            var propertiesArray = jsonResponse["properties"].ToObject<List<PropertyDto>>();

            // Assert
            propertiesArray.Should().NotBeNull();
            propertiesArray?.Count().Should().Be(4);

        }

        [Fact]
        public async Task When_PostPropertyCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
        {
            // Arrange
            string token = JwtTokenBuilder.Create()
                .WithRole("User")
                .WithNameIdentifier()
                .Build();

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Guid ownerId = Guid.NewGuid();
            var ValidProperty = Property.Create(ownerId, streetName, city, region, postalCode, country, propertyType, numberOfRooms, numberOfBathrooms, floor, numberOfFloors, squareFeet, Longitude, Latitude).Value;

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, ValidProperty);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreatePropertyCommandResponse>(responseString);

            result?.Should().NotBeNull();

            var resProperty = result?.Property;

            resProperty?.Should().NotBeNull();
            resProperty?.PropertyId.Should().NotBeEmpty();
            resProperty?.OwnerId.Should().NotBeEmpty();
            resProperty?.StreetName.Should().Be(streetName);
            resProperty?.City.Should().Be(city);
            resProperty?.Region.Should().Be(region);
            resProperty?.PostalCode.Should().Be(postalCode);
            resProperty?.Country.Should().Be(country);
            resProperty?.PropertyType.Should().Be(propertyType);
            resProperty?.NumberOfRooms.Should().Be(numberOfRooms);
            resProperty?.NumberOfBathrooms.Should().Be(numberOfBathrooms);
            resProperty?.Floor.Should().Be(floor);
            resProperty?.NumberOfFloors.Should().Be(numberOfFloors);
            resProperty?.SquareFeet.Should().Be(squareFeet);

        }

        [Fact]
        public async Task When_PostPropertyCommandHandlerIsCalledWithRightParameters_But_No_Authorization_Then_Unauthorized_ShouldBeReturned()
        {
            // Arrange
            Guid ownerId = Guid.NewGuid();
            var ValidProperty = Property.Create(ownerId, streetName, city, region, postalCode, country, propertyType, numberOfRooms, numberOfBathrooms, floor, numberOfFloors, squareFeet, Longitude, Latitude).Value;

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, ValidProperty);

            // Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status401Unauthorized);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateListingCommandResponse>(responseString);

            result.Should().BeNull();

        }

        [Fact]
        public async Task When_PostPropertyCommandHandlerIsCalledWithStreetNameEmpty_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            string token = JwtTokenBuilder.Create()
                .WithRole("User")
                .WithNameIdentifier()
                .Build();

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Guid ownerId = Guid.NewGuid();
            var ValidProperty = Property.Create(ownerId, streetName, city, region, postalCode, country, propertyType, numberOfRooms, numberOfBathrooms, floor, numberOfFloors, squareFeet, Longitude, Latitude).Value;

            var InvalidProperty = ValidProperty;
            InvalidProperty.Update("", city, region, postalCode, country, propertyType, numberOfRooms, numberOfBathrooms, floor, numberOfFloors, squareFeet, Longitude, Latitude);

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, InvalidProperty);

            // Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status400BadRequest);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreatePropertyCommandResponse>(responseString);

            result?.Should().NotBeNull();
            result?.Property.Should().BeNull();
            result?.Success.Should().BeFalse();
            result?.ValidationErrors.Should().NotBeNullOrEmpty();


        }

        [Fact]
        public async Task When_PostPropertyCommandHandlerIsCalledWithSquareFeetLessThan1_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            string token = JwtTokenBuilder.Create()
                .WithRole("User")
                .WithNameIdentifier()
                .Build();

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Guid ownerId = Guid.NewGuid();
            var ValidProperty = Property.Create(ownerId, streetName, city, region, postalCode, country, propertyType, numberOfRooms, numberOfBathrooms, floor, numberOfFloors, squareFeet, Longitude, Latitude).Value;

            var InvalidProperty = ValidProperty;
            int negativeSquareFeet = -1;
            InvalidProperty.Update(streetName, city, region, postalCode, country, propertyType, numberOfRooms, numberOfBathrooms, floor, numberOfFloors, negativeSquareFeet, Longitude, Latitude);

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, InvalidProperty);

            // Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status400BadRequest);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreatePropertyCommandResponse>(responseString);

            result?.Should().NotBeNull();
            result?.Property.Should().BeNull();
            result?.Success.Should().BeFalse();
            result?.ValidationErrors.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task When_PostPropertyCommandHandlerIsCalledWithNumberOfRoomsLessThan1_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            string token = JwtTokenBuilder.Create()
                .WithRole("User")
                .WithNameIdentifier()
                .Build();

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Guid ownerId = Guid.NewGuid();
            var ValidProperty = Property.Create(ownerId, streetName, city, region, postalCode, country, propertyType, numberOfRooms, numberOfBathrooms, floor, numberOfFloors, squareFeet, Longitude, Latitude).Value;

            var InvalidProperty = ValidProperty;
            int negativeNumberOfRooms = -1;
            InvalidProperty.Update(streetName, city, region, postalCode, country, propertyType, negativeNumberOfRooms, numberOfBathrooms, floor, numberOfFloors, squareFeet, Longitude, Latitude);

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, InvalidProperty);

            // Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status400BadRequest);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreatePropertyCommandResponse>(responseString);

            result?.Should().NotBeNull();
            result?.Property.Should().BeNull();
            result?.Success.Should().BeFalse();
            result?.ValidationErrors.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task When_PostPropertyCommandHandlerIsCalledWithNumberOfBathroomsLessThan1_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            string token = JwtTokenBuilder.Create()
                .WithRole("User")
                .WithNameIdentifier()
                .Build();

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Guid ownerId = Guid.NewGuid();
            var ValidProperty = Property.Create(ownerId, streetName, city, region, postalCode, country, propertyType, numberOfRooms, numberOfBathrooms, floor, numberOfFloors, squareFeet, Longitude, Latitude).Value;

            var InvalidProperty = ValidProperty;
            int negativeNumberOfBathrooms = -1;
            InvalidProperty.Update(streetName, city, region, postalCode, country, propertyType, numberOfRooms, negativeNumberOfBathrooms, floor, numberOfFloors, squareFeet, Longitude, Latitude);

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, InvalidProperty);

            // Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status400BadRequest);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreatePropertyCommandResponse>(responseString);

            result?.Should().NotBeNull();
            result?.Property.Should().BeNull();
            result?.Success.Should().BeFalse();
            result?.ValidationErrors.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task When_PostPropertyCommandHandlerIsCalledWithFloorLessThan0_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            string token = JwtTokenBuilder.Create()
                .WithRole("User")
                .WithNameIdentifier()
                .Build();

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Guid ownerId = Guid.NewGuid();
            var ValidProperty = Property.Create(ownerId, streetName, city, region, postalCode, country, propertyType, numberOfRooms, numberOfBathrooms, floor, numberOfFloors, squareFeet, Longitude, Latitude).Value;

            var InvalidProperty = ValidProperty;
            int negativeFloor = -1;
            InvalidProperty.Update(streetName, city, region, postalCode, country, propertyType, numberOfRooms, numberOfBathrooms, negativeFloor, numberOfFloors, squareFeet, Longitude, Latitude);

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, InvalidProperty);

            // Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status400BadRequest);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreatePropertyCommandResponse>(responseString);

            result?.Should().NotBeNull();
            result?.Property.Should().BeNull();
            result?.Success.Should().BeFalse();
            result?.ValidationErrors.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task When_PostPropertyCommandHandlerIsCalledWithNumberOfFloorsLessThan0_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            string token = JwtTokenBuilder.Create()
                .WithRole("User")
                .WithNameIdentifier()
                .Build();

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Guid ownerId = Guid.NewGuid();
            var ValidProperty = Property.Create(ownerId, streetName, city, region, postalCode, country, propertyType, numberOfRooms, numberOfBathrooms, floor, numberOfFloors, squareFeet, Longitude, Latitude).Value;

            var InvalidProperty = ValidProperty;
            int negativeNumberOfFloors = -1;
            InvalidProperty.Update(streetName, city, region, postalCode, country, propertyType, numberOfRooms, numberOfBathrooms, floor, negativeNumberOfFloors, squareFeet, Longitude, Latitude); 

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, InvalidProperty);

            // Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status400BadRequest);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreatePropertyCommandResponse>(responseString);

            result?.Should().NotBeNull();
            result?.Property.Should().BeNull();
            result?.Success.Should().BeFalse();
            result?.ValidationErrors.Should().NotBeNullOrEmpty();
        }

        /*    private static string CreateToken()
            {

                return JwtTokenBuilder.Create()
                    .WithRole("User")
                    .WithNameIdentifier()
                    .Build();

                //return JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(
                //new JwtSecurityToken(
                //    JwtTokenProvider.Issuer,
                //    JwtTokenProvider.Issuer,
                //    new List<Claim> ()
                //    {
                //        new Claim(ClaimTypes.Role, "User"),

                //    },
                //    expires: DateTime.Now.AddMinutes(30),
                //    signingCredentials: JwtTokenProvider.SigningCredentials
                //));



            }*/
    }
}

