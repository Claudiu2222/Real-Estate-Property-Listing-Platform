namespace RealEstatePropertyListingPlatform.Application.Models
{
    public class ImageStorageSettings
    {
        public string ApiKey { get; init; } = default!;
        public string Bucket { get; init; } = default!;

        public string JsonPath { get; init; } = default!;
    }
}
