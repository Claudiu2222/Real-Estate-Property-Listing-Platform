using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePropertyListingPlatform.Application.Contracts
{
    public interface IImageStorageService
    {
        Task<string> GenerateUploadUrlAsync(string filePath);
        Task DeleteImageAsync(string filePath);
    }
}
