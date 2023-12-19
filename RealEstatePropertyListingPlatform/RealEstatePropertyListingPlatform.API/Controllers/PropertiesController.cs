using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.CreateProperty;
using RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.DeleteProperty;
using RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.UpdateProperty;
using RealEstatePropertyListingPlatform.Application.Features.Properties.Queries.GetAllProperties;
using RealEstatePropertyListingPlatform.Application.Features.Properties.Queries.GetBasicInfoByIdProperty;
using RealEstatePropertyListingPlatform.Application.Features.Properties.Queries.GetByIdOwner;
using RealEstatePropertyListingPlatform.Application.Features.Properties.Queries.GetByIdProperty;

namespace RealEstatePropertyListingPlatform.API.Controllers
{

    public class PropertiesController : ApiControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Create(CreatePropertyCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        //[Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllPropertiesQuery());
            
            if (result.Success)
            {
                if (!result.WasFound)
                {
                    return NotFound(result);
                }
 
                return Ok(result);

            }
            else
            {
                return BadRequest(result);
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdPropertyQuery(id));

            if (!result.Success)
            {
                return NotFound(result);
            }
            
            return Ok(result);

        }
        
        [HttpGet("basicinfo/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBasicInfoById(Guid id)
        {
            var result = await Mediator.Send(new GetBasicInfoByIdPropertyQuery(id));

            if (!result.Success)
            {
                return NotFound(result);
            }
            
            return Ok(result);

        }



        [Authorize(Roles = "User")]
        [HttpPut("{idProperty}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid idProperty, UpdatePropertyCommand command)
        {

            
            command.PropertyId = idProperty;
            

            var result = await Mediator.Send(command);
            
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeletePropertyCommand() { PropertyId = id });
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return NoContent();
        }

        [Authorize(Roles = "User")]
        [HttpGet("owner")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdOwner()
        {
            var result = await Mediator.Send(new GetByIdOwnerQuery());

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
