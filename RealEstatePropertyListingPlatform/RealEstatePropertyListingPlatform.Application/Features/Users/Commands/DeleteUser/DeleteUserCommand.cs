using MediatR;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<DeleteUserCommandResponse>
    {
        public DeleteUserCommand(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; } = default!;
    }
}
