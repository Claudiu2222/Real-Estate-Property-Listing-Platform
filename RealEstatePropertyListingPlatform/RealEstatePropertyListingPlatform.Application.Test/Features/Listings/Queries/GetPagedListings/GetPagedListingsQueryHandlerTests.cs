using FluentAssertions;
using NSubstitute;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetPagedListings;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Application.Test.Features.Listings.Queries.GetPagedListings
{
    public class GetPagedListingsQueryHandlerTests : TestBase{
        private readonly GetPagedListingsQueryHandler _handler;

        public GetPagedListingsQueryHandlerTests()
        {
            _handler = new GetPagedListingsQueryHandler(ListingRepository);
        }

        [Fact]
        public async Task When_GetPagedListingsQueryHandlerIsCalled_Then_SuccessIsTrue_And_TotalCountIsReturned()
        {
            // Arrange
            var totalCount = 2;
            var listings = new List<Listing>() { ValidListing1, ValidListing2 };
            ListingRepository.GetCountAsync().Returns(Task.FromResult(Result<int[]>.Success(new[] { totalCount })));
            ListingRepository.GetPagedReponseAsync(Arg.Any<int>(), Arg.Any<int>())
                .Returns(Task.FromResult(Result<IReadOnlyList<Listing>>.Success(listings)));

            var query = new GetPagedListingsQuery(1, 10);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.TotalCount.Should().Be(totalCount);
            result.Listings.Should().NotBeNull();
        }

        [Fact]
        public async Task When_GetPagedListingsQueryHandlerIsCalled_And_PageNumberIsOutOfRange_Then_SuccessIsFalse_And_ValidationErrorsNotNull_IsReturned()
        {
            // Arrange
            var totalCount = 20; // Example total count
            ListingRepository.GetCountAsync().Returns(Task.FromResult(Result<int[]>.Success(new[] { totalCount })));

            var query = new GetPagedListingsQuery(3, 10);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ValidationErrors.Should().NotBeNullOrEmpty();
            result.TotalCount.Should().Be(totalCount);
        }

    }

    }
