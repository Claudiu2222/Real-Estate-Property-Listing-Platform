using MediatR;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand , UpdateUserCommandResponse>
    {
        private readonly IUserRepository repository;

        public UpdateUserCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            var validator = new UpdateUserCommandValidator(repository);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            { 
                return new UpdateUserCommandResponse
                {
                    Success = false,
                    ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            var user = await repository.FindByIdAsync(request.UserId);

            if (!user.IsSuccess)
            {
                return new UpdateUserCommandResponse
                {
                    Success = false,
                    ValidationErrors = new List<string> { user.Error }
                };
            }
            user.Value.Update(request.Email, request.Password, request.FirstName, request.LastName, request.PhoneNumber);


            await repository.UpdateAsync(user.Value);



            return new UpdateUserCommandResponse
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
