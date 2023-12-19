using FluentAssertions;
using NSubstitute;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetPagedListings;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Application.Test.Features.Listings.Queries.GetPagedListings
{
    public class GetPagedListingsQueryHandlerTests : IDisposable
    {
        private readonly GetPagedListingsQueryHandler _handler;
        private readonly IListingRepository _listingRepository;
        private readonly Listing _validListing;
        private readonly Listing _validListing2;
        private readonly Property _validProperty;

        public GetPagedListingsQueryHandlerTests()
        {
            _listingRepository = Substitute.For<IListingRepository>();
            _handler = new GetPagedListingsQueryHandler(_listingRepository);
            _validProperty = Property.Create(Guid.NewGuid(), "Test Address", "Test Zip Code", "Test State", "Test Country", "Romania", Domain.Enums.PropertyType.Apartment, 2, 2, 2, 2, 2).Value;
            _validListing = Listing.Create(_validProperty.OwnerId, _validProperty.PropertyId, "Test Title", new PriceInfo { Value = 100, Currency = Domain.Enums.Currency.USD }, "Test Description", new List<string> { "Test Photo" }, true).Value;
            _validListing2= Listing.Create(_validProperty.OwnerId, _validProperty.PropertyId, "Test Title 2", new PriceInfo { Value = 300, Currency = Domain.Enums.Currency.USD }, "Test Description 2", new List<string> { "Test Photo 2" }, true).Value;
        }

        [Fact]
        public async Task When_GetPagedListingsQueryHandlerIsCalled_Then_SuccessIsTrue_And_TotalCountIsReturned()
        {
            // Arrange
            var totalCount = 2;
            var listings = new List<Listing>() { _validListing, _validListing2 };
            _listingRepository.GetCountAsync().Returns(Task.FromResult(Result<int[]>.Success(new[] { totalCount })));
            _listingRepository.GetPagedReponseAsync(Arg.Any<int>(), Arg.Any<int>())
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
            _listingRepository.GetCountAsync().Returns(Task.FromResult(Result<int[]>.Success(new[] { totalCount })));

            var query = new GetPagedListingsQuery(3, 10);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ValidationErrors.Should().NotBeNullOrEmpty();
            result.TotalCount.Should().Be(totalCount);
        }


        public void Dispose()
        {
            _listingRepository.ClearReceivedCalls();
        }
    }

    }
