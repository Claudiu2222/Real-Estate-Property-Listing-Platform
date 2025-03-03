﻿using Microsoft.AspNetCore.Mvc;
using RealEstatePropertyListingPlatform.Application.Features.Users.Commands.CreateUser;
using RealEstatePropertyListingPlatform.Application.Features.Users.Commands.DeleteUser;
using RealEstatePropertyListingPlatform.Application.Features.Users.Commands.UpdateUser;
using RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetAll;
using RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetById;
using RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetCountUsers;
using RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetPagedUsers;

namespace RealEstatePropertyListingPlatform.API.Controllers
{
    public class UsersControllerOld : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllUsersQuery());
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdUserQuery(id));
            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteUserCommand(id));
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, UpdateUserCommand command)
        {
            if (id != command.UserId)
            {
                return BadRequest($"Cannot update an user with a different id from yours");
            }
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("paginated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetPaginated([FromQuery] int page, [FromQuery] int size) 
        {

            if (page < 1 || size < 1)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new GetPagedUsersQuery(page, size));
            return Ok(result);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCount()
        {
            var result = await Mediator.Send(new GetCountUsersQuery());
            return Ok(result);
        }
       





    }
}
