using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetByIdListing;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Application.Test.Features.Listings.Queries.GetByIdListing
{
    public class GetByIdListingQueryHandlerTests : TestBase
    {
        private readonly GetByIdListingQueryHandler _handler;
      

        public GetByIdListingQueryHandlerTests()
        {
            _handler = new GetByIdListingQueryHandler(ListingRepository);

        }

        [Fact]
        public async Task
            When_GetByIdListingQueryHandlerIsCalled_And_ListingIdIsInvalid_Then_SuccessIsFalse_And_ValidationsErrorsNotNull_AndValueNull_IsReturned()
        {
            // Arrange
            var query = new GetByIdListingQuery(Guid.Empty);
            var failureResult = Result<Listing>.Failure("Listing not found.");
            ListingRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(failureResult));


            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ValidationErrors.Should().NotBeNull();
            result.Listing.Should().BeNull();
        }

        [Fact]
        public async Task
            When_GetByIdListingQueryHandlerIsCalled_And_ListingIdIsValid_Then_SuccessIsTrue_And_ValidationsErrorsNull_AndValueNotNull_IsReturned()
        {
            // Arrange
            var query = new GetByIdListingQuery(Guid.NewGuid());
            var successResult = Result<Listing>.Success(ValidListing1);
            ListingRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(successResult));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.ValidationErrors.Should().BeNull();
            result.Listing.Should().NotBeNull();
        }
    }
}
