using FluentAssertions;
using Newtonsoft.Json.Linq;
using RealEstatePlatform.API.IntegrationTests.Base;
using RealEstatePropertyListingPlatform.Application.Features.Listings;

namespace RealEstatePlatform.API.IntegrationTests.Controllers.ListingsController
{
    public class GetAllListingsController : BaseApplicationContextTests
    {
        private const string RequestUri = "/api/v1/Listings";

        [Fact]
        public async Task When_GetAllListingsQueryHandlerIsCalled_And_There_Are_Listing_In_DB_Then_Success()
        {
            // Arrange && Act
            var response = await Client.GetAsync(RequestUri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(responseString);

            var propertiesArray = jsonResponse["listings"].ToObject<List<ListingDto>>();

            // Assert
            propertiesArray.Should().NotBeNull();
            propertiesArray?.Count().Should().Be(3);

        }


    }
}
