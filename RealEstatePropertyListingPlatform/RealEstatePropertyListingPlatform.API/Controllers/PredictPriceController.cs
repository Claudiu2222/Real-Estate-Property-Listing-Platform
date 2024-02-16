using Microsoft.AspNetCore.Mvc;
using RealEstatePropertyListingPlatform.API.Services;
using RealEstatePropertyListingPlatform.API.Utility;

namespace RealEstatePropertyListingPlatform.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PredictPriceController : ControllerBase
    {
        private readonly PredictPriceService _predictPriceService;

        public PredictPriceController(PredictPriceService predictPriceService)
        {
            _predictPriceService = predictPriceService;
        }

        [HttpPost]
        public IActionResult PredictPrice([FromBody] ModelInput propertyDetails)
        {
            try
            {
                // Faceți predicția folosind serviciul de predicție
                var predictedPrice = _predictPriceService.PredictPrice(propertyDetails);

                // Returnează rezultatul
                return Ok(new { PredictedPrice = predictedPrice, Succes = "true" });
            }
            catch (Exception ex)
            {
                // Tratează orice excepție și returnează un răspuns de eroare
                return BadRequest(new { Message = "Eroare la efectuarea predicției.", Error = ex.Message });
            }
        }
    }
}
