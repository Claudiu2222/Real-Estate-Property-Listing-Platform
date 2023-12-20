using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetPagedListingsById;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Application.Test.Features.Listings.Queries.GetPagedListingsById
{
    public class GetPagedListingsByIdQueryHandlerTests : TestBase
    {
        private readonly GetPagedListingsByIdQueryHandler _handler;
     

        public GetPagedListingsByIdQueryHandlerTests()
        {
            _handler = new GetPagedListingsByIdQueryHandler(ListingRepository, CurrentUserService);
        }

        [Fact]
        public async Task When_GetPagedListingsByIdQueryHandlerIsCalled_Then_SuccessIsTrue_And_TotalCountIsReturned()
        {
            // Arrange
            var userId = Guid.NewGuid();
            CurrentUserService.UserId.Returns(userId.ToString());
            var listings = new List<Listing>() { ValidListing1 };
            ListingRepository.GetListingsByUserId(userId).Returns(listings);

            var query = new GetPagedListingsByIdQuery(1, 10);


            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
            response.TotalCount.Should().Be(listings.Count);
            response.Listings.Should().NotBeNull();
        }

        [Fact]
        public async Task
            When_GetPagedListingsByIdQueryHandlerIsCalled_And_UserIdIsInvalid_Then_SuccessIsFalse_And_ValidationErrorsNotNull_IsReturned()
        {
            // Arrange
            CurrentUserService.UserId.Returns("Invalid User Id");

            var query = new GetPagedListingsByIdQuery(1, 10);

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.Success.Should().BeFalse();
            response.ValidationErrors.Should().Contain("Invalid user id.");
        }

        [Fact]
        public async Task When_GetPagedListingsByIdQueryHandlerIsCalled_And_PageNumberIsOutOfRange_Then_SuccessIsFalse_And_ValidationErrorsNotNull_IsReturned()
               {
        // Arrange
        var userId = Guid.NewGuid();
        CurrentUserService.UserId.Returns(userId.ToString());
        var listings = new List<Listing>();
        ListingRepository.GetListingsByUserId(userId).Returns(listings);

        var query = new GetPagedListingsByIdQuery(2, 10);

        // Act
        var response = await _handler.Handle(query, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        response.ValidationErrors.Should().Contain("Page number out of range");
    }


    }
}
