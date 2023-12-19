using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePropertyListingPlatform.Application.Contracts.Identity;
using RealEstatePropertyListingPlatform.Application.Models.Identity;

namespace RealEstatePropertyListingPlatform.API.Controllers
{
    public class UsersController : ApiControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IAuthService authService, ILogger<UsersController> logger)
        {
            _authService = authService;
            _logger = logger;
        }


        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        [Route("currentuser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, users) = await _authService.GetCurrentUser();

                if (status == 0)
                {
                    return BadRequest();
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBasicInfo(string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }
                
                var (status, user) = await _authService.GetBasicInfo(id);

                return status switch
                {
                    0 => BadRequest(),
                    404 => NotFound(),
                    _ => Ok(user)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut]
        public async Task<IActionResult> Update(UserModel userModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, userUpdated) = await _authService.Update(userModel);

                if (status == 0)
                {
                    return BadRequest();
                }

                return Ok(userUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, users) = await _authService.GetAll();

                if (status == 0)
                {
                    return BadRequest();
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.Delete(id);

                if (status == 0)
                {
                    return BadRequest(message);
                }
                else if (status == 404)
                {
                    return NotFound(message);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
