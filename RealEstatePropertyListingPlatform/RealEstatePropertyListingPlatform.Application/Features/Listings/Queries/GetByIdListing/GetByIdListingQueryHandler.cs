using MediatR;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetByIdListing
{
    public class GetByIdListingQueryHandler : IRequestHandler<GetByIdListingQuery, GetByIdListingQueryResponse>
    {
        private readonly IListingRepository listingRepository;
        public GetByIdListingQueryHandler(IListingRepository listingRepository)
        {
            this.listingRepository = listingRepository;
        }

        public async Task<GetByIdListingQueryResponse> Handle(GetByIdListingQuery request, CancellationToken cancellationToken)
        {
            var listing = await listingRepository.FindByIdAsync(request.Id);
            if(listing.IsSuccess)
            {
                return new GetByIdListingQueryResponse
                {
                    Success = true,
                    Listing = new ListingDto
                    {
                        ListingId = listing.Value.ListingId,
                        ListingCreatorId = listing.Value.ListingCreatorId,
                        PropertyId = listing.Value.PropertyId,
                        Title = listing.Value.Title,
                        Price = listing.Value.Price,
                        Description = listing.Value.Description,
                        Photos = listing.Value.Photos,
                        IsRent = listing.Value.IsRent,
                        Negotiable = listing.Value.Negotiable,
                        DateCreated = listing.Value.DateCreated,
                        DateUpdated = listing.Value.DateUpdated
                    }
                };
            }

            return new GetByIdListingQueryResponse
            {
                Success = false,
                ValidationErrors = new List<string> { listing.Error }
            };
        }
    }
}
