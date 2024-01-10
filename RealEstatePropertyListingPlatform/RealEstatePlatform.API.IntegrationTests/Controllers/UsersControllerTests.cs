using FluentAssertions;
using Newtonsoft.Json.Linq;
using RealEstatePlatform.API.IntegrationTests.Base;
using RealEstatePropertyListingPlatform.Application.Models.Identity;

namespace RealEstatePlatform.API.IntegrationTests.Controllers;

public class UsersControllerTests : BaseApplicationContextTests
{
    private const string RequestUri = "/api/v1/Users";

    [Fact]
    public async Task When_GetAllUsersQueryHandlerIsCalled_Then_Success()
    {
        // Arrange && Act
        var response = await Client.GetAsync(RequestUri);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var jsonResponse = JObject.Parse(responseString);

        var usersArray = jsonResponse["users"]?.ToObject<List<UserModel>>();

        // Assert
        usersArray.Should().NotBeNull();
        usersArray?.Count().Should().Be(4);
    }
    
    private static string CreateToken(string)

}