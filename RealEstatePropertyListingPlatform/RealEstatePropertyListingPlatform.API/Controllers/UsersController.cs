using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePropertyListingPlatform.Application.Contracts.Identity;

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
