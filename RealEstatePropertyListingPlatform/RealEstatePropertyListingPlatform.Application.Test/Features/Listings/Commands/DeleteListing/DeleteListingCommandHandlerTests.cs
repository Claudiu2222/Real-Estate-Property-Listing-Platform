using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.DeleteListing;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Application.Test.Features.Listings.Commands.DeleteListing
{
    public class DeleteListingCommandHandlerTests : IDisposable
    {

        private readonly IListingRepository _listingRepository;
        private readonly DeleteListingCommandHandler _handler;
        private readonly Listing _validListing;
        public DeleteListingCommandHandlerTests()
        {
            _listingRepository = Substitute.For<IListingRepository>();
            _handler = new DeleteListingCommandHandler(_listingRepository);
            _validListing = Listing.Create(Guid.NewGuid(), Guid.NewGuid(), "Test Title", new PriceInfo { Value = 100, Currency = Domain.Enums.Currency.USD }, "Test Description", new List<string> { "Test Photo" }, true).Value;
        }

        [Fact]
        public async Task When_DeleteListingCommandHandlerIsCalled_And_ListingIdIsInvalid_Then_SuccessIsFalse_And_ValidationsErrorsNotNull_IsReturned()
        {
            // Arrange
            var command = new DeleteListingCommand
            {
                ListingId = Guid.Empty
            };
            var failureResult = Result<Listing>.Failure("Listing not found.");
            _listingRepository.DeleteAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(failureResult));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ValidationErrors.Should().NotBeNull();
        }

        [Fact]
        public async Task
            When_DeleteListingCommandHandlerIsCalled_And_ListingIdIsValid_Then_SuccessIsTrue_And_ValidationsErrorsNull_IsReturned()
        {
            // Arrange
            var command = new DeleteListingCommand
            {
                ListingId = Guid.NewGuid()
            };
            var successResult = Result<Listing>.Success(_validListing);
            _listingRepository.DeleteAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(successResult));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.ValidationErrors.Should().BeNull();
        }


        public void Dispose()
        {
            _listingRepository.ClearReceivedCalls();
        }
    }


}
