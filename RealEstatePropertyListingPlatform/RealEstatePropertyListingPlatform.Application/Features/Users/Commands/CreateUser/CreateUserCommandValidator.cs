using FluentValidation;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private static readonly int MaxStringLength = 100;
        private static readonly int MinPasswordLength = 8;

        public CreateUserCommandValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is required.")
                .NotNull()
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password is required.")
                .NotNull()
                .MinimumLength(MinPasswordLength).WithMessage($"Password must be at least {MinPasswordLength} characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("LastName is required.")
                .NotNull()
                .MaximumLength(MaxStringLength).WithMessage($"LastName cannot exceed {MaxStringLength} characters.");

            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("FirstName is required.")
                .NotNull()
                .MaximumLength(MaxStringLength).WithMessage($"FirstName cannot exceed {MaxStringLength} characters.");
            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required.")
                .NotNull()
                .MinimumLength(10).MaximumLength(10).WithMessage("PhoneNumber must be 10 digits long.");
        }
    }
}
