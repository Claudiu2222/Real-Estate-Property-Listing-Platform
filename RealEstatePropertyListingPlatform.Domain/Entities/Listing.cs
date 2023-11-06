using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Domain.Entities
{
    public class Listing
    {
        public Guid ListingId { get; private set; }
        public Guid ListingCreatorId { get; private set; }
        public Money Price { get; private set; }

        private Listing()
        {

        }

        public static Result<Listing> Create(Guid id, Money price)
        {
           throw new NotImplementedException();
        }
    }
}
