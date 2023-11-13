using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserCommandResponse>
    {

        private readonly IUserRepository repository;

        public DeleteUserCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }
        public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteUserCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new DeleteUserCommandResponse
                {
                    Success = false,
                    ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            var user = await repository.FindByIdAsync(request.UserId);
            if (user.Value == null)
            {
                return new DeleteUserCommandResponse
                {
                    Success = false,
                    ValidationErrors = new List<string> { "User does not exist" }
                };
            }

            await repository.DeleteAsync(user.Value.UserId);

            return new DeleteUserCommandResponse
            {
                Success = true,
            };
        }
    }
}
