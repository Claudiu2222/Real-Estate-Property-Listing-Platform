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

    
    public class CreateListingCommandHandlerTests : IDisposable
    {
        private readonly CreateListingCommandHandler _handler;
        private readonly IListingRepository _listingRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPropertyRepository _propertyRepository;
        private readonly Property _validProperty;


        public CreateListingCommandHandlerTests()
        {
            _listingRepository = Substitute.For<IListingRepository>();
            _currentUserService = Substitute.For<ICurrentUserService>();
            _propertyRepository = Substitute.For<IPropertyRepository>();
            _handler = new CreateListingCommandHandler(_listingRepository, _currentUserService, _propertyRepository);

            _validProperty = Property.Create(Guid.NewGuid(), "Test Address", "Test Zip Code", "Test State", "Test Country", "Romania", PropertyType.Apartment, 2, 2, 2, 2, 2).Value;
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
            _propertyRepository.FindByIdAsync(Arg.Any<Guid>())
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
            _currentUserService.UserId.Returns("Invalid User Id");

            var validPropertyResult = Result<Property>.Success(_validProperty); 
            _propertyRepository.FindByIdAsync(Arg.Any<Guid>())
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
            _currentUserService.UserId.Returns(_validProperty.OwnerId.ToString());

            var validPropertyResult = Result<Property>.Success(_validProperty); 
            _propertyRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult(validPropertyResult));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.ValidationErrors.Should().BeNullOrEmpty();
            result.Listing.Should().NotBeNull();
        }

        public void Dispose()
        {
            _listingRepository.ClearReceivedCalls();
            _currentUserService.ClearReceivedCalls();
            _propertyRepository.ClearReceivedCalls();
        }

    }

    
}
