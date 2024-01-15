using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace RealEstatePropertyListingPlatform.API.Services
{
    public class AzureBlobService
    {
        BlobServiceClient blobServiceClient;
        BlobContainerClient blobContainerClient;
        string azureConnectionString = "DefaultEndpointsProtocol=https;AccountName=realestateproperty;AccountKey=bdk8SXNtRmtPYZoGlnVEH505JN9KkJ0pFGCGlAVsrSnE+Xm+1sH5mZQCMEfQ6ZuN16uL4qpbKPa7+AStD03apg==;EndpointSuffix=core.windows.net";

        public AzureBlobService()
        {
            blobServiceClient = new BlobServiceClient(azureConnectionString);
            blobContainerClient = blobServiceClient.GetBlobContainerClient("realestatepropertylistingcontainer");
        }

        public async Task<List<BlobContentInfo>> UploadFiles(List<IFormFile> files)
        {
            var azureResponse = new List<BlobContentInfo>();

            foreach (var file in files)
            {
                string fileName = file.FileName;
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var client = await blobContainerClient.UploadBlobAsync(fileName, memoryStream, default);
                    azureResponse.Add(client);
                    
                }
            };
            return azureResponse;
        }

        public async Task<List<BlobItem>> GetUploadedBlob()
        {
            var items = new List<BlobItem>();

            var UploadedFiles = blobContainerClient.GetBlobsAsync();
            await foreach(BlobItem file in UploadedFiles)
            {
                items.Add(file);
            }
            return items;
        }

    }
}
