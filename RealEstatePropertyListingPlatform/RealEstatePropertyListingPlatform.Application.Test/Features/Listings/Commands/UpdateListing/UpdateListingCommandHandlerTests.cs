using FluentAssertions;
using NSubstitute;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.UpdateListing;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Application.Test.Features.Listings.Commands.UpdateListing
{
    public class UpdateListingCommandHandlerTests : TestBase
    {
        private readonly UpdateListingCommandHandler _handler;
      

        public UpdateListingCommandHandlerTests()
        {
            _handler = new UpdateListingCommandHandler(ListingRepository, CurrentUserService, PropertyRepository);
        }

        [Fact]
        public async Task
            When_UpdateListingCommandHandlerIsCalled_And_PropertyIdIsInvalid_Then_SuccessIsFalse_And_ValidationsErrorsNotNull_IsReturned()
        {
            // Arrange
            var command = new UpdateListingCommand
            {
                ListingId = Guid.Empty,
                ListingCreatorId = Guid.Empty,
                PropertyId = Guid.Empty,
                Title = "Test Title",
                Price = new PriceInfo { Value = 100, Currency = Currency.USD },
                Description = "Test Description",
                Photos = new List<string> { "Test Photo" },
                Negotiable = true
            };
            var failurePropertyResult = Result<Property>.Failure("Property not found.");
            PropertyRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(failurePropertyResult));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ValidationErrors.Should().NotBeNull();
        }

        [Fact]
        public async Task
            When_UpdateListingCommandHandlerIsCalled_And_PropertyIdIsValid_And_ListingIdIsInvalid_Then_SuccessIsFalse_And_ValidationsErrorsNotNull_IsReturned()
        {

            // Arrange
            var command = new UpdateListingCommand
            {
                ListingId = Guid.Empty,
                ListingCreatorId = Guid.Empty,
                PropertyId = Guid.Empty,
                Title = "Test Title",
                Price = new PriceInfo { Value = 100, Currency = Currency.USD },
                Description = "Test Description",
                Photos = new List<string> { "Test Photo" },
                Negotiable = true
            };
            var successPropertyResult = Result<Property>.Success(ValidProperty);
            PropertyRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(successPropertyResult));

            var failureListingResult = Result<Listing>.Failure("Listing not found.");
            ListingRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(failureListingResult));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ValidationErrors.Should().NotBeNull();
        }


        [Fact]
        public async Task
            When_UpdateListingCommandHandlerIsCalled_And_PropertyIdIsValid_And_ListingIdIsValid_Then_SuccessIsTrue_And_ValidationsErrorsNull_IsReturned()
        {
            // Arrange
            var command = new UpdateListingCommand
            {
                ListingId = ValidListing1.ListingId,
                ListingCreatorId = ValidListing1.ListingCreatorId,
                PropertyId = ValidListing1.PropertyId,
                Title = "Test Title",
                Price = new PriceInfo { Value = 100, Currency = Currency.USD },
                Description = "Test Description",
                Photos = new List<string> { "Test Photo" },
                Negotiable = true
            };

            CurrentUserService.UserId.Returns(ValidListing1.ListingCreatorId.ToString());

            var successPropertyResult = Result<Property>.Success(ValidProperty);
            PropertyRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(successPropertyResult));

            var successListingResult = Result<Listing>.Success(ValidListing1);
            ListingRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(successListingResult));
            //modify
            ValidListing1.Update(command.Title, command.Price,command.Description,command.Photos,command.Negotiable);

            ListingRepository.UpdateAsync(Arg.Any<Listing>())
                .Returns(Task.FromResult(Result<Listing>.Success(ValidListing1)));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.ValidationErrors.Should().BeNull();
            result.Listing.Should().NotBeNull();
            result.Listing.Title.Should().Be(command.Title);
            result.Listing.Price.Should().Be(command.Price);
            result.Listing.Description.Should().Be(command.Description);
            result.Listing.Photos.Should().BeEquivalentTo(command.Photos);
            result.Listing.Negotiable.Should().Be(command.Negotiable);

        }
    }
}