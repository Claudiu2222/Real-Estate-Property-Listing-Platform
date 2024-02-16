namespace RealEstatePropertyListingPlatform.Application.Contracts
{
    public interface IImageStorageService
    {
        Task<string> GenerateUploadUrlAsync(string fileName);
        Task DeleteImageAsync(string fileName);
    }
}
