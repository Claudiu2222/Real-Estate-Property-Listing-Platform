using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstatePropertyListingPlatform.Application.Contracts;

namespace RealEstatePropertyListingPlatform.API.Controllers
{
    public class UrlResponse
    {
        public string Url { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IImageStorageService _imageStorageService;

        public UploadController(IImageStorageService imageStorageService)
        {
            _imageStorageService = imageStorageService;
        }

        [HttpGet("generate-upload-url")]
        public async Task<IActionResult> GenerateUploadUrl(string filePath)
        {

            if (string.IsNullOrEmpty(filePath))
            {
                return BadRequest("Invalid file path");
            }


            var preSignedUrl = await _imageStorageService.GenerateUploadUrlAsync(filePath);

            var response = new UrlResponse
            {
                Url = preSignedUrl
            };
            if (!string.IsNullOrEmpty(preSignedUrl))
            {
                return Ok(response);
            }
            else
            {
                return BadRequest("Could not generate the upload URL.");
            }
        }

        [HttpDelete("{fileName}")]
        public async Task<IActionResult> Delete(string fileName)
        {
            try
            {
                await _imageStorageService.DeleteImageAsync(fileName);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}