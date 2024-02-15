using RealEstatePropertyListingPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetFilteredPagedListings
{
    public class GetFilteredPagedListingsResponse : BaseResponse
    {
        public List<ListingDto> Listings { get; set; }
        public int TotalCount { get; set; }
    }
}
