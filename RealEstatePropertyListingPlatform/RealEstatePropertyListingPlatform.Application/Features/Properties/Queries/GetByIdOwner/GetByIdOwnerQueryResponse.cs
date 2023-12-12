using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Queries.GetByIdOwner
{
    public class GetByIdOwnerQueryResponse : BaseResponse
    {
        public List<PropertyDto> Properties { get; set; }
    }
}
