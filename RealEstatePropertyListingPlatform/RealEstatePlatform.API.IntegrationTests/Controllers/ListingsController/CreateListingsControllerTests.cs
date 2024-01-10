using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RealEstatePlatform.API.IntegrationTests.Base;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.CreateListing;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;
using RealEstatePropertyListingPlatform.Domain.Records;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RealEstatePlatform.API.IntegrationTests.Controllers.ListingsController
{
    public class CreateListingsControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "/api/v1/Listings";

        private static PriceInfo price = new() { Value = 1000, Currency = Currency.USD };
        private static string title = "Listing-test-OK";
        private static string description = "Description-OK";
        private static List<string> photos = new() { "link/to/photo" };
        private static bool isNegotiable = true;
        private static Guid listingCreatorId = JwtTokenBuilder.UserId;
        private static Guid propertyId = Seed.ValidPropertyId;

        private static Listing ValidListing = Listing.Create(listingCreatorId, propertyId, title, price, description, photos, isNegotiable).Value;


        [Fact]
        public async Task When_PostListingCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
        {
            // Arrange
            string token = JwtTokenBuilder.Create()
                .WithRole("User")
                .WithNameIdentifier()
                .Build();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, ValidListing);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateListingCommandResponse>(responseString);

            result.Should().NotBeNull();
            result?.Listing.Should().NotBeNull();
            result?.Listing?.ListingId.Should().NotBeEmpty();
            result?.Listing?.ListingCreatorId.Should().Be(listingCreatorId);
            result?.Listing?.PropertyId.Should().Be(propertyId);
            result?.Listing?.Title.Should().Be(title);
            result?.Listing?.Price.Should().BeEquivalentTo(price);
            result?.Listing?.Description.Should().Be(description);
            result?.Listing?.Photos.Should().BeEquivalentTo(photos);
            result?.Listing?.Negotiable.Should().Be(isNegotiable);

        }

        [Fact]
        public async Task When_PostListingCommandHandlerIsCalledWithRightParameters_But_No_Authorization_Then_Unauthorized_ShouldBeReturned()
        {
            // Arrange

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, ValidListing);

            // Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status401Unauthorized);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateListingCommandResponse>(responseString);

            result.Should().BeNull();

        }

        [Fact]
        public async Task When_PostListingCommandHandlerIsCalledWithEmptyTitle_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            string token = JwtTokenBuilder.Create()
                .WithRole("User")
                .WithNameIdentifier()
                .Build();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var InvalidListing = ValidListing;
            InvalidListing.Update("", price, description, photos, isNegotiable);

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, InvalidListing);

            // Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status400BadRequest);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateListingCommandResponse>(responseString);

            result.Should().NotBeNull(); // an error message should be returned
            result?.Listing.Should().BeNull(); // no listing should be returned
            result?.Success.Should().BeFalse(); // the request should not be successful
            result?.ValidationErrors.Should().NotBeNullOrEmpty(); // there should be validation errors
        }


        [Fact]
        public async Task When_PostListingCommandHandlerIsCalledWithEmptyDescription_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            string token = JwtTokenBuilder.Create()
                .WithRole("User")
                .WithNameIdentifier()
                .Build();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var InvalidListing = ValidListing;
            InvalidListing.Update(title, price, string.Empty, photos, isNegotiable);

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, InvalidListing);

            // Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status400BadRequest);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateListingCommandResponse>(responseString);

            result.Should().NotBeNull(); // an error message should be returned
            result?.Listing.Should().BeNull(); // no listing should be returned
            result?.Success.Should().BeFalse(); // the request should not be successful
            result?.ValidationErrors.Should().NotBeNullOrEmpty(); // there should be validation errors
        }

        [Fact]
        public async Task When_PostListingCommandHandlerIsCalledWithNegativePrice_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            string token = JwtTokenBuilder.Create()
                .WithRole("User")
                .WithNameIdentifier()
                .Build();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var InvalidListing = ValidListing;
            PriceInfo negativePrice = new() { Value = -1000, Currency = Currency.USD };
            InvalidListing.Update(title, negativePrice, description, photos, isNegotiable);

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, InvalidListing);

            // Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status400BadRequest);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateListingCommandResponse>(responseString);

            result.Should().NotBeNull(); // an error message should be returned
            result?.Listing.Should().BeNull(); // no listing should be returned
            result?.Success.Should().BeFalse(); // the request should not be successful
            result?.ValidationErrors.Should().NotBeNullOrEmpty(); // there should be validation errors
        }

        [Fact]
        public async Task When_PostListingCommandHandlerIsCalledWithUnexistingCurrency_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            string token = JwtTokenBuilder.Create()
                .WithRole("User")
                .WithNameIdentifier()
                .Build();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var InvalidListing = ValidListing;
            PriceInfo negativePrice = new() { Value = 1000, Currency = (Currency)100};
            InvalidListing.Update(title, negativePrice, description, photos, isNegotiable);

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, InvalidListing);

            // Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status400BadRequest);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateListingCommandResponse>(responseString);

            result.Should().NotBeNull(); // an error message should be returned
            result?.Listing.Should().BeNull(); // no listing should be returned
            result?.Success.Should().BeFalse(); // the request should not be successful
            result?.ValidationErrors.Should().NotBeNullOrEmpty(); // there should be validation errors
        }

        [Fact]
        public async Task When_PostListingCommandHandlerIsCalledWithEmptyListOfPhotos_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            string token = JwtTokenBuilder.Create()
                .WithRole("User")
                .WithNameIdentifier()
                .Build();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var InvalidListing = ValidListing;
            InvalidListing.Update(title, price, description, [], isNegotiable);

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, InvalidListing);

            // Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status400BadRequest);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateListingCommandResponse>(responseString);

            result.Should().NotBeNull(); // an error message should be returned
            result?.Listing.Should().BeNull(); // no listing should be returned
            result?.Success.Should().BeFalse(); // the request should not be successful
            result?.ValidationErrors.Should().NotBeNullOrEmpty(); // there should be validation errors
        }

        [Fact]
        public async Task When_PostListingCommandHandlerIsCalledWithListOfPhotosWhichContainAnEmptyLink_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            string token = JwtTokenBuilder.Create()
                .WithRole("User")
                .WithNameIdentifier()
                .Build();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var InvalidListing = ValidListing;
            InvalidListing.Update(title, price, description, ["ok/link" , ""], isNegotiable);

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, InvalidListing);

            // Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status400BadRequest);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateListingCommandResponse>(responseString);

            result.Should().NotBeNull(); // an error message should be returned
            result?.Listing.Should().BeNull(); // no listing should be returned
            result?.Success.Should().BeFalse(); // the request should not be successful
            result?.ValidationErrors.Should().NotBeNullOrEmpty(); // there should be validation errors
        }

        [Fact]
        public async Task When_PostListingCommandHandlerIsCalledWithUnexistingPropertyId_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            string token = JwtTokenBuilder.Create()
                .WithRole("User")
                .WithNameIdentifier()
                .Build();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var InvalidListing = Listing.Create(listingCreatorId, Guid.NewGuid(), title, price, description, photos, isNegotiable).Value;

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, InvalidListing);

            // Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status400BadRequest);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateListingCommandResponse>(responseString);

            result.Should().NotBeNull(); // an error message should be returned
            result?.Listing.Should().BeNull(); // no listing should be returned
            result?.Success.Should().BeFalse(); // the request should not be successful
            result?.ValidationErrors.Should().NotBeNullOrEmpty(); // there should be validation errors
        }


    }
}
