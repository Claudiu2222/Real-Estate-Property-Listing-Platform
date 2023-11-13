using MediatR;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IUserRepository repository;

        public CreateUserCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateUserCommandResponse
                {
                    Success = false,
                    ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            var user = User.Create(request.Email, request.Password, request.FirstName, request.LastName, request.PhoneNumber);
            if (!user.IsSuccess)
            {
                return new CreateUserCommandResponse
                {
                    Success = false,
                    ValidationErrors = new List<string> { user.Error }
                };
            }

            await repository.AddAsync(user.Value);

            return new CreateUserCommandResponse
            {
                Success = true,
                User = new UserDto
                {
                    UserId = user.Value.UserId,
                    Email = user.Value.Email,
                    LastName = user.Value.LastName,
                    FirstName = user.Value.FirstName,
                    PhoneNumber = user.Value.PhoneNumber
                }
            };
        }
    }
        
}
