using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using RealEstatePropertyListingPlatform.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.UpdateListing;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Application.Test.Features.Listings.Commands.UpdateListing
{
    public class UpdateListingCommandHandlerTests : IDisposable
    {
        private readonly IListingRepository _listingRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPropertyRepository _propertyRepository;
        private readonly UpdateListingCommandHandler _handler;
        private readonly Property _validProperty;
        private readonly Listing _validListing;

        public UpdateListingCommandHandlerTests()
        {
            _listingRepository = Substitute.For<IListingRepository>();
            _currentUserService = Substitute.For<ICurrentUserService>();
            _propertyRepository = Substitute.For<IPropertyRepository>();
            _handler = new UpdateListingCommandHandler(_listingRepository, _currentUserService, _propertyRepository);

            _validProperty = Property.Create(Guid.NewGuid(), "Test Address", "Test Zip Code", "Test State",
                "Test Country", "Romania", PropertyType.Apartment, 2, 2, 2, 2, 2).Value;
            _validListing = Listing.Create(_validProperty.OwnerId, _validProperty.PropertyId, "Test Title",
                new PriceInfo { Value = 100, Currency = Currency.USD }, "Test Description",
                new List<string> { "Test Photo" }, true).Value;
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
            _propertyRepository.FindByIdAsync(Arg.Any<Guid>())
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
            var successPropertyResult = Result<Property>.Success(_validProperty);
            _propertyRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(successPropertyResult));

            var failureListingResult = Result<Listing>.Failure("Listing not found.");
            _listingRepository.FindByIdAsync(Arg.Any<Guid>())
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
                ListingId = _validListing.ListingId,
                ListingCreatorId = _validListing.ListingCreatorId,
                PropertyId = _validListing.PropertyId,
                Title = "Test Title",
                Price = new PriceInfo { Value = 100, Currency = Currency.USD },
                Description = "Test Description",
                Photos = new List<string> { "Test Photo" },
                Negotiable = true
            };

            _currentUserService.UserId.Returns(_validListing.ListingCreatorId.ToString());

            var successPropertyResult = Result<Property>.Success(_validProperty);
            _propertyRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(successPropertyResult));

            var successListingResult = Result<Listing>.Success(_validListing);
            _listingRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(successListingResult));
            //modify
            _validListing.Update(command.Title, command.Price,command.Description,command.Photos,command.Negotiable);

            _listingRepository.UpdateAsync(Arg.Any<Listing>())
                .Returns(Task.FromResult(Result<Listing>.Success(_validListing)));

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

        public void Dispose()
        {
            _listingRepository.ClearReceivedCalls();
            _currentUserService.ClearReceivedCalls();
            _propertyRepository.ClearReceivedCalls();
        }
    }
}