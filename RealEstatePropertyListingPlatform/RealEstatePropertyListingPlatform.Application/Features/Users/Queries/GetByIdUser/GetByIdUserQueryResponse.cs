using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetByIdUser
{
    public class GetByIdUserQueryResponse : BaseResponse
    {

        public UserDto User { get; set; }

    }
}
