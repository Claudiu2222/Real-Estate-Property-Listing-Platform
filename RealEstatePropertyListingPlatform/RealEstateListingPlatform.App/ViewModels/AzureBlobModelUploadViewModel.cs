using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace RealEstateListingPlatform.App.ViewModels
{
    public class AzureBlobModelUploadViewModel
    {
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
}
