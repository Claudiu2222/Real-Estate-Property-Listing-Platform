using RealEstateListingPlatform.App.Services.Responses;

namespace RealEstateListingPlatform.App.Contracts
{
    public interface IImageUploadService
    {
        Task<ApiResponseImageUpload> UploadImageToStorage(String presignedUrl, ByteArrayContent content );

        Task<ApiResponseImageUpload> GetPresignedUrl(String imgName);

        Task<ApiResponseImageUpload> DeleteImage(String imgName);

        String GetFileUrl(String imgName);
    }
}
