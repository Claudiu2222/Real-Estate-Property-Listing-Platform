using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Contracts
{
    public interface IAzureBlobService
    {
        Task<ApiResponseAzureBlobUpload> UploadFile(AzureBlobModelUploadViewModel azureBlobModelUpload);
    }
}
