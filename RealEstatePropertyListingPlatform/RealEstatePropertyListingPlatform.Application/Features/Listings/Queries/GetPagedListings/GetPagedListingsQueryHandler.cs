using MediatR;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetPagedListings
{
    public class GetPagedListingsQueryHandler : IRequestHandler<GetPagedListingsQuery, GetPagedListingsResponse>
    {

        private readonly IListingRepository listingRepository;

        public GetPagedListingsQueryHandler(IListingRepository listingRepository)
        {
            this.listingRepository = listingRepository;

        }

        public async Task<GetPagedListingsResponse> Handle(GetPagedListingsQuery request, CancellationToken cancellationToken)
        {

            var totalCount = await listingRepository.GetCountAsync();

            if (totalCount.Value[0] == 0)
            {
                return new GetPagedListingsResponse
                {
                    Success = true,
                    Message = "No listings found",
                    TotalCount = 0,
                };
            }

            if (totalCount.Value[0] <= (request.PageNumber - 1) * request.PageSize)
            {
                return new GetPagedListingsResponse
                {
                    Success = false,
                    ValidationErrors = new List<string> { "Page number out of range" },
                    TotalCount = totalCount.Value[0],
                };
            }
            
            
            var result = await listingRepository.GetPagedReponseAsync(request.PageNumber, request.PageSize);



            return new GetPagedListingsResponse
            {
                Success = true,
                Listings = result.Value.Select(x => new ListingDto
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
                    DateCreated = x.DateCreated,
                    DateUpdated = x.DateUpdated,
                    IsRent = x.IsRent,
                    Negotiable = x.Negotiable
                }).ToList(),
                TotalCount = totalCount.Value[0],
            };


        }
    }
}
