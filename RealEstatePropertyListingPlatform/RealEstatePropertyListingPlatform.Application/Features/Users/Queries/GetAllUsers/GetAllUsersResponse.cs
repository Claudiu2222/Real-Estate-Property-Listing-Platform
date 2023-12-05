using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetAll
{
    public class GetAllUsersResponse : BaseResponse
    {
        public List<UserDto> Users { get; set; }
        public bool WasFound { get; set; }
    }
}
