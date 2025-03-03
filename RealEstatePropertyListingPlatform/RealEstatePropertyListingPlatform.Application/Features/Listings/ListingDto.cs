﻿using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings
{
    public class ListingDto
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
        public bool IsRent { get; set; }
        public bool Negotiable { get; set; }
    }
}
