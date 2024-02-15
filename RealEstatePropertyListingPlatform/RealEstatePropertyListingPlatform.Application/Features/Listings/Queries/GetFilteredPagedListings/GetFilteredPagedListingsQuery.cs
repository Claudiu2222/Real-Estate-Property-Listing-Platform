using MediatR;
using RealEstatePropertyListingPlatform.Domain.Enums;
using RealEstatePropertyListingPlatform.Domain.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetFilteredPagedListings
{
    public class GetFilteredPagedListingsQuery: IRequest<GetFilteredPagedListingsResponse>
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public decimal PriceLowerBound { get; set; }
        public decimal PriceUpperBound { get; set; }
        public Currency PriceCurrency { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public int PropertyType { get; set; }
        public int SquareFeetLowerBound { get; set; }
        public int SquareFeetUpperBound { get; set; }
        public bool? ForRent { get; set; }
        public string? ContainsInTitle { get; set; }

    }
}
