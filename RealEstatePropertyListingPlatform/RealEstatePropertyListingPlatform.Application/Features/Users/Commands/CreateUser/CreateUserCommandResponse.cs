using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandResponse : BaseResponse
    {
        public CreateUserCommandResponse() : base()
        {
        }

        public UserDto User { get; set; }
    }
}
