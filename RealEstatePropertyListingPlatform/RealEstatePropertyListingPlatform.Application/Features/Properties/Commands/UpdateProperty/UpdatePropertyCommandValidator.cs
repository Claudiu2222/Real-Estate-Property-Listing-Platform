using FluentValidation;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.ClassValidators;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IPropertyRepository propertyRepository;

        public UpdatePropertyCommandValidator(IUserRepository userRepository, IPropertyRepository propertyRepository) {
            this.userRepository = userRepository;
            this.propertyRepository = propertyRepository;

            RuleFor(p => p.OwnerId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(OwnerExists).WithMessage("Owner does not exist.");


            RuleFor(p => p)
                .MustAsync(BeAValidOwner).WithMessage("You are not the owner of this property.");


            RuleFor(p => p)
                .Must(command => ValidateProperty(command) == null)
                .WithMessage(ValidateProperty);



        }

        private async Task<bool> OwnerExists(Guid ownerId, CancellationToken cancellationToken)
        {
            var resultUser = await userRepository.FindByIdAsync(ownerId);

            if (!resultUser.IsSuccess)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> BeAValidOwner(UpdatePropertyCommand command, CancellationToken cancellationToken)
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

        }

        private string ValidateProperty(UpdatePropertyCommand command)
        {
            var possError = PropertyValidator.ValidateProperty(command.StreetName, command.City,
                command.Region, command.PostalCode, command.Country, command.PropertyType, command.NumberOfRooms,
                command.NumberOfBathrooms, command.Floor, command.NumberOfFloors, command.SquareFeet);

            return possError;
        
        }

    }
}
