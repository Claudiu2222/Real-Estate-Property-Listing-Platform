using RealEstatePropertyListingPlatform.Domain.Enums;

namespace RealEstatePropertyListingPlatform.Domain.Records
{
    public record PriceInfo
    {
        public decimal Value { get; init; }
        public Currency Currency { get; init; }


    }
}
