using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.CreateListing;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Application.Test.Features.Listings.Commands.CreateListing
{

    
    public class CreateListingCommandHandlerTests : TestBase
    {
        private readonly CreateListingCommandHandler _handler;


        public CreateListingCommandHandlerTests()
        {
            _handler = new CreateListingCommandHandler(ListingRepository, CurrentUserService, PropertyRepository);
               }

        [Fact]
        public async Task When_CreateListingCommandHandlerIsCalled_And_PropertyIdIsInvalid_Then_SuccessIsFalse_And_ValidationsErrorsNotNull_AndValueNull_IsReturned()
        {
            // Arrange
            var command = new CreateListingCommand
            {
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
            result.ValidationErrors.Should().NotBeNullOrEmpty();
            result.Listing.Should().BeNull();
        }

        [Fact]
        public async Task
            When_CreateListingCommandHandlerIsCalled_And_PropertyIdIsValid_And_CreatorIdNotValid_Then_SuccessIsFalse_And_ValidationsErrorsNotNull_AndValueNull_IsReturned()
        {
            // Arrange
            var command = new CreateListingCommand
            {
                PropertyId = Guid.NewGuid(),
                Title = "Test Title",
                Price = new PriceInfo { Value = 100, Currency = Currency.USD },
                Description = "Test Description",
                Photos = new List<string> { "Test Photo" },
                Negotiable = true
            };
            CurrentUserService.UserId.Returns("Invalid User Id");

            var validPropertyResult = Result<Property>.Success(ValidProperty); 
            PropertyRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(validPropertyResult)); 

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ValidationErrors.Should().NotBeNullOrEmpty();
            result.Listing.Should().BeNull();
        }

        [Fact]
        public async Task When_CreateListingCommandHandlerIsCalled_And_PropertyIdIsValid_And_CreatorIdIsValid_And_ListingIsValid_Then_SuccessIsTrue_And_ValidationsErrorsNull_AndValueNotNull_IsReturned()
        {
            // Arrange
            var command = new CreateListingCommand
            {
                PropertyId = Guid.NewGuid(),
                Title = "Test Title",
                Price = new PriceInfo { Value = 100, Currency = Currency.USD },
                Description = "THIS IS A DESCRIPTION",
                Photos = new List<string> { "Test Photo" },
                Negotiable = true
            };
            CurrentUserService.UserId.Returns(ValidProperty.OwnerId.ToString());

            var validPropertyResult = Result<Property>.Success(ValidProperty); 
            PropertyRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(validPropertyResult));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.ValidationErrors.Should().BeNullOrEmpty();
            result.Listing.Should().NotBeNull();
        }

    

    }

    
}
