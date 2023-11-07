using RealEstatePropertyListingPlatform.Domain.ClassValidators;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Domain.Entities
{
    public class Listing : AuditableEntity
    {
        public Guid ListingId { get; private set; }
        public Guid ListingCreatorId { get; private set; }
        public Guid PropertyId { get; private set; }
        public string? Title { get; private set; }
        public Money? Price { get; private set; }
        public string? Description { get; private set; }
        public List<string>? Photos { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateUpdated { get; private set; }
        public bool Negotiable { get; private set; }



        private Listing() { }

        public static Result<Listing> Create(Guid listingCreatorId, Guid propertyId, string
            title, Money price, string description, List<string> photos, bool negotiable)
        {
            var error = ListingValidator.ValidateListing(title, price, description, photos);

            if (string.IsNullOrWhiteSpace(error))
            {
                return Result<Listing>.Failure(error);
            }

            var dateNow = DateTime.UtcNow;
            var listing = new Listing
            {
                ListingId = new Guid(),
                ListingCreatorId = listingCreatorId,
                PropertyId = propertyId,
                Title = title,
                Price = price,
                Description = description,
                Photos = photos,
                DateCreated = dateNow,
                DateUpdated = dateNow,
                Negotiable = negotiable
            };

            return Result<Listing>.Success(listing);
        }

        public Result<Listing> UpdatePrice(Money price)
        {
            var error = ListingValidator.ValidateMoney(price);
            if (!string.IsNullOrWhiteSpace(error)) return Result<Listing>.Failure(error);
            Price = price;
            DateUpdated = DateTime.UtcNow;
            return Result<Listing>.Success(this);
        }
        
        public Result<Listing> UpdateTitle(string title)
        {
            var error = ListingValidator.ValidateString(title, nameof(title));
            if (!string.IsNullOrWhiteSpace(error)) return Result<Listing>.Failure(error);
            Title = title;
            DateUpdated = DateTime.UtcNow;
            return Result<Listing>.Success(this);
        }
        
        public Result<Listing> UpdateDescription(string description)
        {
            var error = ListingValidator.ValidateString(description, nameof(description));
            if (!string.IsNullOrWhiteSpace(error)) return Result<Listing>.Failure(error);
            Description = description;
            DateUpdated = DateTime.UtcNow;
            return Result<Listing>.Success(this);
        }
        
        public Result<Listing> UpdatePhotos(List<string> photos)
        {
            var error = ListingValidator.ValidatePhotos(photos);
            if (!string.IsNullOrWhiteSpace(error)) return Result<Listing>.Failure(error);
            Photos = photos;
            DateUpdated = DateTime.UtcNow;
            return Result<Listing>.Success(this);
        }
        
        public Result<Listing> ToggleNegotiable()
        {
            Negotiable = !Negotiable;
            DateUpdated = DateTime.UtcNow;
            return Result<Listing>.Success(this);
        }
    }
}
