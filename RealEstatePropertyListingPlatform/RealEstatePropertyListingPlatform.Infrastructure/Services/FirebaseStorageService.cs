using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;
using RealEstatePropertyListingPlatform.Application.Contracts;
using RealEstatePropertyListingPlatform.Application.Models;

namespace RealEstatePropertyListingPlatform.Infrastructure.Services
{
    public class FirebaseStorageService : IImageStorageService
    {
        private readonly string _bucketName;
        private readonly StorageClient _storageClient;
        private readonly string _jsonPath;

        public FirebaseStorageService(IOptions<ImageStorageSettings> firebaseSettings)
        {
             _bucketName = firebaseSettings.Value.Bucket;
             _jsonPath = firebaseSettings.Value.JsonPath;
             _storageClient = new StorageClientBuilder
             {
                 CredentialsPath = _jsonPath
             }.Build();
        }

        public async Task<string> GenerateUploadUrlAsync(string filePath)
        {
            var signer = UrlSigner.FromServiceAccountPath(_jsonPath);
            var url = await signer.SignAsync(_bucketName, filePath, TimeSpan.FromMinutes(15), HttpMethod.Put);

            return url;
        }
        public async Task DeleteImageAsync(string filePath)
        {
            await _storageClient.DeleteObjectAsync(_bucketName, filePath);
        }
    }

}

