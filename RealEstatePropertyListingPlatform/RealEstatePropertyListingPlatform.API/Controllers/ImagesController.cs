using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using RealEstatePropertyListingPlatform.Application.Contracts;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using WebAPI.Services;

namespace RealEstatePropertyListingPlatform.API.Controllers
{
    public class UrlResponse
    {
        public string Url { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageStorageService _imageStorageService;
        private readonly ICurrentUserService _currentUserService;

        public ImagesController(IImageStorageService imageStorageService, ICurrentUserService currentUserService)
        {
            _imageStorageService = imageStorageService;
            _currentUserService = currentUserService;
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
        public async Task<IActionResult> DeleteImage(string fileName)
        {
            try
            {
                var currentUserId = _currentUserService.GetCurrentClaimsPrincipal().GetNameIdentifierId();
                if (fileName == null)
                {
                    return BadRequest("Invalid file name");
                }

                if (fileName.Split('_').First() != currentUserId.ToString())
                {
                    return Forbid();
                }
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