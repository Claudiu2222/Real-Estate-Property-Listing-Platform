using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RealEstatePlatform.API.IntegrationTests.Base;
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
            propertiesArray.Count().Should().Be(4);

        }

        [Fact]
        public async Task When_PostPropertyCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
        {
            // Arrange
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var property = Property.Create(
                Guid.NewGuid(),
                "Test Street",
                "Test City",
                "Test Region",
                "Test Postal Code",
                "Test Country",
                PropertyType.Apartment,
                2,
                1,
                1,
                3,
                1000
            ).Value;


            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, property);
 
            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PropertyDto>(responseString);
            result?.PropertyId.Should().NotBeEmpty();
            result?.OwnerId.Should().NotBeEmpty();
            result?.Should().NotBeNull();
            result?.StreetName.Should().Be(property.StreetName);
            result?.City.Should().Be(property.City);
            result?.Region.Should().Be(property.Region);
            result?.PostalCode.Should().Be(property.PostalCode);
            result?.Country.Should().Be(property.Country);
            result?.PropertyType.Should().Be(property.PropertyType);
            result?.NumberOfRooms.Should().Be(property.NumberOfRooms);
            result?.NumberOfBathrooms.Should().Be(property.NumberOfBathrooms);
            result?.Floor.Should().Be(property.Floor);
            result?.NumberOfFloors.Should().Be(property.NumberOfFloors);
            result?.SquareFeet.Should().Be(property.SquareFeet);

        }



        private static string CreateToken()
        {

            return JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(
            new JwtSecurityToken(
                JwtTokenProvider.Issuer,
                JwtTokenProvider.Issuer,
                new List<Claim> ()
                {
                    new Claim(ClaimTypes.Role, "User"),
                    new ("department", "IT")
                
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: JwtTokenProvider.SigningCredentials
            ));
        }
    }
}
