using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetPagedUsers
{
    public class GetPagedUsersResponse : BaseResponse
    {
        public List<UserDto> Users { get; set; }
        public int TotalCount { get; set; }
    }
}
