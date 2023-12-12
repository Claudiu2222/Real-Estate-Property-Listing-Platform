using FluentValidation;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.CreateListing
{
    public class CreateListingCommandValidator : AbstractValidator<CreateListingCommand>
    {
        private static readonly int MaxStringLength = 100;

        private readonly IPropertyRepository propertyRepository;
        public CreateListingCommandValidator(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;


            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(MaxStringLength).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(p => p.Price.Currency).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(MaxStringLength).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");
            RuleFor(p=> p.Price.Value).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.");

            RuleFor(p => p.Photos)
                .Must(p => p.Count > 0).WithMessage("{PropertyName} must contain at least one photo.");

            RuleFor(p => p.DateCreated)
                .Must(p => p.Date <= DateTime.Now.Date).WithMessage("{PropertyName} must be less than or equal to today's date.");

            RuleFor(p => p.DateUpdated)
                .Must(p => p.Date <= DateTime.Now.Date).WithMessage("{PropertyName} must be less than or equal to today's date.");

            RuleFor(p => p.Description)
                .MinimumLength(10).WithMessage("{PropertyName} must be at least {MinLength} characters.");

            RuleFor(p=> p.PropertyId)
                .MustAsync(BeAValidPropertyId)
                .WithMessage("{PropertyName} must be a valid property id.");

        }



        private async Task<bool> BeAValidPropertyId(Guid propertyId, CancellationToken cancellationToken)
        {
            var property = await propertyRepository.FindByIdAsync(propertyId);

            return property.IsSuccess;
        }

    }
}
