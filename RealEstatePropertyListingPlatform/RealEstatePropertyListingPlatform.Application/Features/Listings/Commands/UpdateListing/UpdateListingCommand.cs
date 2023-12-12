using MediatR;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.UpdateListing
{
    public class UpdateListingCommand : IRequest<UpdateListingCommandResponse>
    {
        public Guid ListingId { get; set; }
        public Guid ListingCreatorId { get; set; }
        public Guid PropertyId { get; set; }
        public string Title { get; set; } = default!;
        public PriceInfo Price { get; set; } = default!;
        public string Description { get; set; } = default!;
        public List<string> Photos { get; set; } = default!;
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool Negotiable { get; set; }
    }
}
