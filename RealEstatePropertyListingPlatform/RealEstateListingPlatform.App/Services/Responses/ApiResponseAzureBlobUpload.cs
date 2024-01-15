using Azure.Storage.Blobs.Models;

namespace RealEstateListingPlatform.App.Services.Responses
{
    public class ApiResponseAzureBlobUpload
    {
        List<BlobContentInfo> blobContentInfos = new List<BlobContentInfo>();
        public bool Success { get; set; }

    }
}
