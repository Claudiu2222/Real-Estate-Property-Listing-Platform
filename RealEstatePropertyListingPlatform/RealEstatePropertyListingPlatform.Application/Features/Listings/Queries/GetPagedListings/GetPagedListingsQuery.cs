using MediatR;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetPagedListings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetPagedListings
{
    public class GetPagedListingsQuery : IRequest<GetPagedListingsResponse>
    {

        public GetPagedListingsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }


    }
}
