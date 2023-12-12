using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandResponse:BaseResponse
    {

        public UserDto User { get; set; }

    }
}
