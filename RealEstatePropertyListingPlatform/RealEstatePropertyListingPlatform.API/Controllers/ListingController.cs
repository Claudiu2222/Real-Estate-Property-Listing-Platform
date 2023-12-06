using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePropertyListingPlatform.API.Controllers;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.CreateListing;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.DeleteListing;

namespace RealEstateListingListingPlatform.API.Controllers
{
    public class ListingController : ApiControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Create(CreateListingCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /*[Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllListingsQuery());

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
            var result = await Mediator.Send(new GetByIdListingQuery(id));

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);

        }



        [Authorize(Roles = "User")]
        [HttpPut("{idListing}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid idListing, UpdateListingCommand command)
        {


            command.ListingId = idListing;


            var result = await Mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }*/
        

        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteListingCommand() { ListingId = id });
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return NoContent();
        }
    }
}
