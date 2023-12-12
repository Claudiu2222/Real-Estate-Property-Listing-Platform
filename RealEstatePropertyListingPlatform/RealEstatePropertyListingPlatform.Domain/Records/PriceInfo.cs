namespace RealEstatePropertyListingPlatform.Domain.Records
{
    public record PriceInfo
    {
        public decimal Value { get; init; }
        public string Currency { get; init; }
    }
}
