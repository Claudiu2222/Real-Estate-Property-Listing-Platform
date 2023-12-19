using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Queries.GetBasicInfoByIdProperty;

public class GetBasicInfoByIdPropertyQueryResponse: BaseResponse
{

    public PropertyDto Property { get; set; }

}