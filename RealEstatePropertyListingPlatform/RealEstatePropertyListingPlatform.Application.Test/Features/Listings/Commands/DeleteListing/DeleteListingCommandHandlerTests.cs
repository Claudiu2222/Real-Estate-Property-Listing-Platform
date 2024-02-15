using System.Net;
using FluentAssertions;
using Google;
using NSubstitute;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.DeleteListing;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Application.Test.Features.Listings.Commands.DeleteListing
{
    public class DeleteListingCommandHandlerTests : TestBase
    {
        private readonly DeleteListingCommandHandler _handler;
        public DeleteListingCommandHandlerTests()
        {
            _handler = new DeleteListingCommandHandler(ListingRepository, ImageStorageService);
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
            ListingRepository.DeleteAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(failureResult));
            ImageStorageService.When(x => x.DeleteImageAsync(Arg.Any<string>()))
                .Do(x => { throw new GoogleApiException("Test API Key", "Image not found."); });

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
            var successResult = Result<Listing>.Success(ValidListing1);
            ListingRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(successResult));
            ListingRepository.DeleteAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(successResult));
            ImageStorageService.DeleteImageAsync(Arg.Any<string>())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.ValidationErrors.Should().BeNull();
        }

    }


}
