using MediatR;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UpdateUserCommandResponse>
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;
    }
}
