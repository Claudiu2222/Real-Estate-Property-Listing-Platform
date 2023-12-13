using MediatR;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetPagedListings;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetPagedListingsById
{
    public class GetPagedListingsByIdQueryHandler : IRequestHandler<GetPagedListingsByIdQuery, GetPagedListingsByIdResponse>
    {

        private readonly IListingRepository listingRepository;
        private readonly ICurrentUserService currentUserService;

        public GetPagedListingsByIdQueryHandler(IListingRepository listingRepository, ICurrentUserService currentUserService)
        {
            this.listingRepository = listingRepository;
            this.currentUserService = currentUserService;


        }

        public async Task<GetPagedListingsByIdResponse> Handle(GetPagedListingsByIdQuery request,
            CancellationToken cancellationToken)
        {
            var currentUserIdClaim = currentUserService.UserId;
            Guid currentUserId;
            try
            {
                currentUserId = Guid.Parse(currentUserIdClaim);
            }
            catch (Exception)
            {
                return new GetPagedListingsByIdResponse
                {
                    Success = false,
                    ValidationErrors = new List<string> { "Invalid user id." }
                };
            }

            var result = await listingRepository.GetListingsByUserId(currentUserId);

            var totalCount = result.Count();

            result = result
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            if (totalCount < (request.PageNumber - 1) * request.PageSize)
            {
                return new GetPagedListingsByIdResponse
                {
                    Success = false,
                    ValidationErrors = new List<string> { "Page number out of range" },
                    TotalCount = totalCount
                };
            }


            return new GetPagedListingsByIdResponse
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