using FluentValidation;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("UserId is required.");
        }
    }
}
