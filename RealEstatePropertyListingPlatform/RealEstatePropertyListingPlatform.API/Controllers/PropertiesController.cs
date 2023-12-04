using Microsoft.AspNetCore.Mvc;
using RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.CreateProperty;

namespace RealEstatePropertyListingPlatform.API.Controllers
{

    public class PropertiesController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreatePropertyCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
