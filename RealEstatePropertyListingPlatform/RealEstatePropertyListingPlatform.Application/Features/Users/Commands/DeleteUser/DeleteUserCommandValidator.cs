using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
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
