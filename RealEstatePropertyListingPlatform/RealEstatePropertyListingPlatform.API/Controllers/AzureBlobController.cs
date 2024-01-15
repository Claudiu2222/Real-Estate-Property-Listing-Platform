using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstatePropertyListingPlatform.API.Services;

namespace RealEstatePropertyListingPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureBlobController : ControllerBase
    {
        public AzureBlobService _azureBlobService;
        public AzureBlobController(AzureBlobService azureBlobService)
        {
            _azureBlobService = azureBlobService;            
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            var response = await _azureBlobService.UploadFiles(files);
            return Ok(response);
        }

        [HttpGet("BlobList")]
        public async Task<IActionResult> GetUploadedBlob()
        {
            var response = await _azureBlobService.GetUploadedBlob();
            return Ok(response);
        }
    }
}
