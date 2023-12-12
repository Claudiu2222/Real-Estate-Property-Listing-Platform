using FluentValidation;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        private static readonly int MaxStringLength = 100;

        //private readonly ICurrentUserService currentUserService;
        public CreatePropertyCommandValidator()
        {
            //this.currentUserService = currentUserService;

            RuleFor(p => p.StreetName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(MaxStringLength).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(p => p.City)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(MaxStringLength).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(p => p.Region)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(MaxStringLength).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(p => p.PostalCode)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(MaxStringLength).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(p => p.Country)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(MaxStringLength).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(p => p.PropertyType)
                .IsInEnum().WithMessage("{PropertyName} must be a valid property type.");
            //also check that is within the enum range

            RuleFor(p => p.NumberOfRooms)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.");

            RuleFor(p => p.NumberOfBathrooms)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.");

            RuleFor(p => p.NumberOfFloors)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.");

            RuleFor(p => p.Floor)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater or equal than 0.")
                .LessThanOrEqualTo(p => p.NumberOfFloors).WithMessage("{PropertyName} must be less than or equal to the number of total floors ({ComparisonValue}).");

            RuleFor(p => p.SquareFeet)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

            //check that the ownerId exists in the database
/*            RuleFor(p => p.OwnerId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .Must(BeAValidUser).WithMessage("{PropertyName} must be a valid user (the OwnerId should already exist).");
*/

        }

/*        private bool BeAValidUser(Guid OwnerId)
        {
            var currentUserIdClaim = currentUserService.UserId;

            if (Guid.TryParse(currentUserIdClaim, out Guid currentUserId))
            {
                return OwnerId == currentUserId;
            }

            return false;

        }*/

    }
}
