using FluentValidation;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.ClassValidators;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.UpdateListing
{
    public class UpdateListingCommandValidator : AbstractValidator<UpdateListingCommand>
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IPropertyRepository propertyRepository;
        public UpdateListingCommandValidator(ICurrentUserService currentUserService, IPropertyRepository propertyRepository)
        {
            this.currentUserService = currentUserService;
            this.propertyRepository = propertyRepository;

            RuleFor(p => p.ListingCreatorId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Must(BeAValidUser).WithMessage("{PropertyName} must be the same as the current user.");

            RuleFor(p => p.PropertyId)
                .MustAsync(BeAValidPropertyId)
                .WithMessage("{PropertyName} must be a valid property id.");

            RuleFor(p => p)
                .Must(command => ValidateListing(command) == null)
                .WithMessage(ValidateListing);
        }
        private bool BeAValidUser(Guid OwnerId)
        {
            var currentUserIdClaim = currentUserService.UserId;

            if (Guid.TryParse(currentUserIdClaim, out Guid currentUserId))
            {
                return OwnerId == currentUserId;
            }

            return false;

        }

        private async Task<bool> BeAValidPropertyId(Guid propertyId, CancellationToken cancellationToken)
        {
            var property = await propertyRepository.FindByIdAsync(propertyId);

            return property.IsSuccess;
        }

        private string ValidateListing(UpdateListingCommand command)
        {
            var possError = ListingValidator.ValidateListing(command.Title,command.Price, command.Description, command.Photos);

            return possError;

        }
    }

}
