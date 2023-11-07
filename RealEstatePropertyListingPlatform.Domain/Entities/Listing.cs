using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstatePropertyListingPlatform.Domain.ClassValidators;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Domain.Entities
{
    public class Listing
    {
        public Guid ListingId { get; private set; }
        public Guid ListingCreatorId { get; private set; }
        public Guid PropertyId { get; private set; }
        public string Title { get; private set; }
        public Money Price { get; private set; }
        public string Description { get; private set; }
        public List<string> Photos { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateUpdated { get; private set; }
        public bool Negotiable { get; private set; }



        private Listing(){ }

        public static Result<Listing> Create(Guid listingCreatorId, Guid propertyId, string title, Money price, string description, List<string> photos, DateTime dateCreated, bool negotiable)
        {
            string error = ListingValidator.ValidateListing(title, price, description, photos, dateCreated);

            if (string.IsNullOrWhiteSpace(error))
            {
                return Result<Listing>.Failure(error);
            }

            var listing = new Listing
            {
                ListingId = new Guid(),
                ListingCreatorId = listingCreatorId,
                PropertyId = propertyId,
                Title = title,
                Price = price,
                Description = description,
                Photos = photos,
                DateCreated = dateCreated,
                Negotiable = negotiable
            };

            return Result<Listing>.Success(listing);
        }
    }
}
