using FluentValidation;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private static readonly int MaxStringLength = 100;
        private static readonly int MinPasswordLength = 8;
        private readonly IUserRepository userRepository;

        public UpdateUserCommandValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(MinPasswordLength).WithMessage("{PropertyName} must be at least {MinPasswordLength} characters long.")
                .Matches("[A-Z]").WithMessage("{PropertyName} must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("{PropertyName} must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("{PropertyName} must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("{PropertyName} must contain at least one special character.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(MaxStringLength).WithMessage("{PropertyName} cannot exceed {MaxStringLength} characters.");

            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(MaxStringLength).WithMessage("{PropertyName} cannot exceed {MaxStringLength} characters.");
            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(10).MaximumLength(10).WithMessage("{PropertyName} must be 10 digits long.");
            RuleFor(p => p.PhoneNumber)
        .NotEmpty().WithMessage("{PropertyName} is required.")
        .NotNull()
        .MinimumLength(10).MaximumLength(10).WithMessage("{PropertyName} must be 10 digits long.");


            RuleFor(p => p.Email)
                .MustAsync(EmailExists).WithMessage("Email : {PropertyValue} already exists.");

            RuleFor(p => p.PhoneNumber)
                .MustAsync(PhoneNumberExists).WithMessage("Phone number : {PropertyValue} already exists.");
        }

        private async Task<bool> EmailExists(string email, CancellationToken token)
        {
            var result = await userRepository.FindByEmailAsync(email);
            return !result.IsSuccess;
        }

        private async Task<bool> PhoneNumberExists(string phoneNumber, CancellationToken token)
        {
            var result = await userRepository.FindByPhoneNumberAsync(phoneNumber);
            return !result.IsSuccess;
        }
    }
}
