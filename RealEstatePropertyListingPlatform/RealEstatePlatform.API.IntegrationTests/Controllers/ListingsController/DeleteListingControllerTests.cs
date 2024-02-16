using FluentAssertions;
using Newtonsoft.Json.Linq;
using RealEstatePlatform.API.IntegrationTests.Base;
using RealEstatePropertyListingPlatform.Application.Features.Listings;
using System.Net.Http.Headers;

namespace RealEstatePlatform.API.IntegrationTests.Controllers.ListingsController
{
    public class DeleteListingControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "/api/v1/Listings";

        [Fact]
        public async Task When_DeleteListingCommandHandlerIsCalled_Then_Succes_Is_Returned_And_Get_All_Decreases()
        {
            // Arrange
            string token = JwtTokenBuilder.Create()
                .WithRole("User")
                .WithNameIdentifier()
                .Build();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            // Act

            //Delete a listing with a valid id
            var response = await Client.DeleteAsync($"{RequestUri}/{Seed.ValidListingId}");
            var responseGetAll = await Client.GetAsync(RequestUri);
            // Assert
            response.EnsureSuccessStatusCode();

            var responseString = await responseGetAll.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(responseString);

            var propertiesArray = jsonResponse["listings"].ToObject<List<ListingDto>>();

            // Assert
            propertiesArray.Should().NotBeNull();
            propertiesArray?.Count().Should().Be(2); //because we deleted one listing

        }

    }
}
