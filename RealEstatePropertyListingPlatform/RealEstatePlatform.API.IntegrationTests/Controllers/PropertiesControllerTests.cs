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
            propertiesArray?.Count().Should().Be(4);

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
            var result = JsonConvert.DeserializeObject<CreatePropertyCommandResponse>(responseString);
            
            result?.ValidationErrors.Should().BeNullOrEmpty();

            var resProperty = result?.Property;

            resProperty?.PropertyId.Should().NotBeEmpty();
            resProperty?.OwnerId.Should().NotBeEmpty();
            resProperty?.Should().NotBeNull();
            resProperty?.StreetName.Should().Be(property.StreetName);
            resProperty?.City.Should().Be(property.City);
            resProperty?.Region.Should().Be(property.Region);
            resProperty?.PostalCode.Should().Be(property.PostalCode);
            resProperty?.Country.Should().Be(property.Country);
            resProperty?.PropertyType.Should().Be(property.PropertyType);
            resProperty?.NumberOfRooms.Should().Be(property.NumberOfRooms);
            resProperty?.NumberOfBathrooms.Should().Be(property.NumberOfBathrooms);
            resProperty?.Floor.Should().Be(property.Floor);
            resProperty?.NumberOfFloors.Should().Be(property.NumberOfFloors);
            resProperty?.SquareFeet.Should().Be(property.SquareFeet);

        }



        private static string CreateToken()
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



        }
    }
}
