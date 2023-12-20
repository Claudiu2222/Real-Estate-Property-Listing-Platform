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
    public class GetAllListingsQueryHandlerTests : TestBase
    {
        private readonly GetAllListingsQueryHandler _handler;
        public GetAllListingsQueryHandlerTests()
        {
           _handler = new GetAllListingsQueryHandler(ListingRepository);
        }

        [Fact]
        public async Task When_GetAllListingsQueryHandlerIsCalled_Then_SuccessIsTrue_And_ListingsNotNull_IsReturned()
        {
            // Arrange
            var query = new GetAllListingsQuery();
            var listings = new List<Listing>
            {
                ValidListing1
            };
            ListingRepository.GetAllAsync()
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
            ListingRepository.GetAllAsync()
                .Returns(Task.FromResult(Result<IReadOnlyList<Listing>>.Success(listings)));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Listings.Should().NotBeNull();
            result.Listings.Should().HaveCount(0);
        }
    }
}
