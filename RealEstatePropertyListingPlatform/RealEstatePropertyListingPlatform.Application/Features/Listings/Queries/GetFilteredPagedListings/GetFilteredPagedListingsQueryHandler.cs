using MediatR;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetPagedListingsById;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetFilteredPagedListings
{
    public class GetFilteredPagedListingsQueryHandler : IRequestHandler<GetFilteredPagedListingsQuery, GetFilteredPagedListingsResponse>
    {
        private readonly IListingRepository listingRepository;

        public GetFilteredPagedListingsQueryHandler(IListingRepository listingRepository)
        {
            this.listingRepository = listingRepository;
        }

        public async Task<GetFilteredPagedListingsResponse> Handle(GetFilteredPagedListingsQuery request, CancellationToken cancellationToken)
        {
            var result = await listingRepository.GetFilteredListingsAsync(request.PriceLowerBound, request.PriceUpperBound, (int)request.PriceCurrency,
                request.City!, request.Region!, request.PropertyType, request.SquareFeetLowerBound, request.SquareFeetUpperBound, request.ForRent, request.ContainsInTitle!);
            
            var totalCount = result.Count;

            result = result
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            if (totalCount <= (request.PageNumber - 1) * request.PageSize)
            {
                return new GetFilteredPagedListingsResponse
                {
                    Success = false,
                    ValidationErrors = new List<string> { "Page number out of range" },
                    TotalCount = totalCount
                };
            }


            return new GetFilteredPagedListingsResponse
            {
                Success = true,
                Listings = result.Select(x => new ListingDto
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
                    Negotiable = x.Negotiable
                }).ToList(),
                TotalCount = totalCount,
            };


        }
    }

}
