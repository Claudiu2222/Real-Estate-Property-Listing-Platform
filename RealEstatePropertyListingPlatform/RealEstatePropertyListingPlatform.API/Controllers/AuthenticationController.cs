using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePropertyListingPlatform.Application.Contracts.Identity;
using RealEstatePropertyListingPlatform.Application.Models.Identity;
using RealEstatePropertyListingPlatform.Identity.Models;

namespace RealEstatePropertyListingPlatform.API.Controllers
{

    public class AuthenticationController : ApiControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.Login(model);

                if (status == 0)
                {
                    return BadRequest(message);
                }

                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("register/user")]
        public async Task<IActionResult> RegisterUser(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.Registeration(model, UserRole.User);

                if (status == 0)
                {
                    return BadRequest(message);
                }

                // nu ar trebui sa transmitem inapoi si parola
                model.Password = "Psswd shouldn't be seen";

                return CreatedAtAction(nameof(RegisterUser), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Admin)]
        [Route("register/admin")]
        public async Task<IActionResult> RegisterAdmin(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.Registeration(model, UserRole.Admin);

                if (status == 0)
                {
                    return BadRequest(message);
                }

                // nu ar trebui sa transmitem inapoi si parola
                model.Password = "Psswd shouldn't be seen";

                return CreatedAtAction(nameof(RegisterAdmin), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
