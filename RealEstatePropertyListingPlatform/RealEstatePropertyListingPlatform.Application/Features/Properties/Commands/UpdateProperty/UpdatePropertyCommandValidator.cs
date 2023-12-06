using FluentValidation;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.ClassValidators;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IPropertyRepository propertyRepository;

        public UpdatePropertyCommandValidator(ICurrentUserService currentUserService, IPropertyRepository propertyRepository) {
            this.currentUserService = currentUserService;
            this.propertyRepository = propertyRepository;

            RuleFor(p => p.OwnerId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Must(BeAValidUser).WithMessage("{PropertyName} must be the same as the current user.");    

/*
            RuleFor(p => p)
                .MustAsync(BeAValidOwner).WithMessage("You are not the owner of this property.");
*/

            RuleFor(p => p)
                .Must(command => ValidateProperty(command) == null)
                .WithMessage(ValidateProperty);



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

        /*private async Task<bool> OwnerExists(Guid ownerId, CancellationToken cancellationToken)
        {
            var resultUser = await userRepository.FindByIdAsync(ownerId);

            if (!resultUser.IsSuccess)
            {
                return false;
            }
            return true;
        }*/

        /*private async Task<bool> BeAValidOwner(UpdatePropertyCommand command, CancellationToken cancellationToken)
        {
            var resultProperty = await propertyRepository.FindByIdAsync(command.PropertyId);
            
            if (resultProperty.IsSuccess)
            {
                if (resultProperty.Value.OwnerId == command.OwnerId)
                {
                    return true;
                }

                return false;
            
            }
            
            return false;

        }*/

        private string ValidateProperty(UpdatePropertyCommand command)
        {
            var possError = PropertyValidator.ValidateProperty(command.StreetName, command.City,
                command.Region, command.PostalCode, command.Country, command.PropertyType, command.NumberOfRooms,
                command.NumberOfBathrooms, command.Floor, command.NumberOfFloors, command.SquareFeet);

            return possError;
        
        }

    }
}
