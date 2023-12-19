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
    public class GetPagedListingsByIdQueryHandlerTests : IDisposable
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IListingRepository _listingRepository;
        private readonly GetPagedListingsByIdQueryHandler _handler;
        private readonly Property _validProperty;
        private readonly Listing _validListing;

        public GetPagedListingsByIdQueryHandlerTests()
        {
            _currentUserService = Substitute.For<ICurrentUserService>();
            _listingRepository = Substitute.For<IListingRepository>();
            _handler = new GetPagedListingsByIdQueryHandler(_listingRepository, _currentUserService);
            _validProperty = Property.Create(Guid.NewGuid(), "Test Address", "Test Zip Code", "Test State",
                "Test Country", "Romania", Domain.Enums.PropertyType.Apartment, 2, 2, 2, 2, 2).Value;
            _validListing = Listing.Create(_validProperty.OwnerId, _validProperty.PropertyId, "Test Title",
                new PriceInfo { Value = 100, Currency = Domain.Enums.Currency.USD }, "Test Description",
                new List<string> { "Test Photo" }, true).Value;
        }

        [Fact]
        public async Task When_GetPagedListingsByIdQueryHandlerIsCalled_Then_SuccessIsTrue_And_TotalCountIsReturned()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _currentUserService.UserId.Returns(userId.ToString());
            var listings = new List<Listing>() { _validListing };
            _listingRepository.GetListingsByUserId(userId).Returns(listings);

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
            _currentUserService.UserId.Returns("Invalid User Id");

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
        _currentUserService.UserId.Returns(userId.ToString());
        var listings = new List<Listing>();
        _listingRepository.GetListingsByUserId(userId).Returns(listings);

        var query = new GetPagedListingsByIdQuery(2, 10);

        // Act
        var response = await _handler.Handle(query, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        response.ValidationErrors.Should().Contain("Page number out of range");
    }

    public void Dispose()
        {
            _currentUserService.ClearReceivedCalls();
            _listingRepository.ClearReceivedCalls();
        }



    }
}
