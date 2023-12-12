using MediatR;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetByIdListing
{
    public record GetByIdListingQuery(Guid Id) : IRequest<GetByIdListingQueryResponse>
    {
    }
}
