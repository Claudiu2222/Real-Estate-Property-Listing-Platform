using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using RealEstatePropertyListingPlatform.Application.Features.Listings;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetAllListings;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Application.Test.Features.Listings.Queries.GetAllListings
{
    public class GetAllListingsQueryHandlerTests : IDisposable
    {
        private readonly IListingRepository _listingRepository;
        private readonly GetAllListingsQueryHandler _handler;
        private readonly Listing _validListing;
        private readonly Property _validProperty;
        public GetAllListingsQueryHandlerTests()
        {
            _listingRepository = Substitute.For<IListingRepository>();
            _handler = new GetAllListingsQueryHandler(_listingRepository);
            _validProperty= Property.Create(Guid.NewGuid(), "Test Address", "Test Zip Code", "Test State", "Test Country", "Romania", PropertyType.Apartment, 2, 2, 2, 2, 2).Value;
            _validListing = Listing.Create(_validProperty.OwnerId, _validProperty.PropertyId, "Test Title", new PriceInfo { Value = 100, Currency = Currency.USD }, "Test Description", new List<string> { "Test Photo" }, true).Value;

        }

        [Fact]
        public async Task When_GetAllListingsQueryHandlerIsCalled_Then_SuccessIsTrue_And_ListingsNotNull_IsReturned()
        {
            // Arrange
            var query = new GetAllListingsQuery();
            var listings = new List<Listing>
            {
                _validListing
            };
            _listingRepository.GetAllAsync()
                .Returns(Task.FromResult(Result<IReadOnlyList<Listing>>.Success(listings)));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Listings.Should().NotBeNull();
            result.Listings.Should().HaveCount(1);

        }

        [Fact]
        public async Task
            When_GetAllListingsQueryHandlerIsCalled_And_ListingsAreEmpty_Then_SuccessTrue_And_ListingsNotNull_And_ListingsCountIsZero_IsReturned()
        {
            // Arrange
            var query = new GetAllListingsQuery();
            var listings = new List<Listing>();
            _listingRepository.GetAllAsync()
                .Returns(Task.FromResult(Result<IReadOnlyList<Listing>>.Success(listings)));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Listings.Should().NotBeNull();
            result.Listings.Should().HaveCount(0);
        }
        public void Dispose()
        {
            _listingRepository.ClearReceivedCalls();
        }
    }
}
