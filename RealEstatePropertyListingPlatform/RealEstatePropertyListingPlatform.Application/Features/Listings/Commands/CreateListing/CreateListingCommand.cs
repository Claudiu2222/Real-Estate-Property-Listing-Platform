using MediatR;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.CreateListing
{
    public class CreateListingCommand : IRequest<CreateListingCommandResponse>
    {
        public Guid PropertyId { get; set; }
        public string Title { get; set; } = default!;
        public PriceInfo Price { get; set; } = default!;
        public string Description { get; set; } = default!;
        public List<string> Photos { get; set; } = default!;
        public bool IsRent { get; set; }
        public bool Negotiable { get; set; }
    }
}
