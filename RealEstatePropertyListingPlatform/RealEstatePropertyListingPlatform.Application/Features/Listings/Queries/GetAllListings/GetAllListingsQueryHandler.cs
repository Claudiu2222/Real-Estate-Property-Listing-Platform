using MediatR;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetAllListings
{
    public class GetAllListingsQueryHandler : IRequestHandler<GetAllListingsQuery, GetAllListingsQueryResponse>
    {
        private readonly IListingRepository listingRepository;

        public GetAllListingsQueryHandler(IListingRepository listingRepository)
        {
            this.listingRepository = listingRepository;
        }

        public async Task<GetAllListingsQueryResponse> Handle(GetAllListingsQuery request, CancellationToken cancellationToken)
        {
            GetAllListingsQueryResponse response = new();

            var result = await listingRepository.GetAllAsync();

            if(result.IsSuccess)
            {
                response.Success = true;
                response.Listings = result.Value.Select(x => new ListingDto
                {
                    ListingId = x.ListingId,
                    ListingCreatorId = x.ListingCreatorId,
                    PropertyId = x.PropertyId,
                    Title = x.Title,
                    Price = new PriceInfo
                    {
                        Value = x.Price.Value,
                        Currency = x.Price.Currency
                    },
                    Description = x.Description,
                    Photos = x.Photos,
                    Negotiable = x.Negotiable,
                    DateCreated = x.DateCreated,
                    DateUpdated = x.DateUpdated
                }).ToList();

                response.WasFound = response.Listings.Count != 0;

                if(!response.WasFound)
                {
                    response.Message = "No listings found";
                }

                return response;
            }

            response.ValidationErrors = new List<string> { result.Error };

            return response;
        }
    }
}
