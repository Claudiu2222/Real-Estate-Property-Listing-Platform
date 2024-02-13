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

        //change password
        [Authorize(Roles = "Admin, User")]
        [HttpPut]
        [Route("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.ChangePassword(changePasswordModel);

                if (status == 0)
                {
                    return BadRequest(new { IsSuccess = false, message = message });
                }
                else if (status == 404)
                {
                    return NotFound(new { IsSuccess = false, message = message });
                }

                return Ok(new { IsSuccess = true, message = message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("resetpasswordnotconnected")]
        public async Task<IActionResult> ResetPassword(ChangePasswordNotConnectedModel resetPasswordModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.ChangePasswordNotConnected(resetPasswordModel);

                if (status == 0)
                {
                    return BadRequest(new { IsSuccess = false, message = "Invalid payload" });
                }
                else if (status == 404)
                {
                    return BadRequest(new { IsSuccess = false, message = "User not found" });
                }

                return Ok(new {IsSuccess = true, message = "Password changed successfully"});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("isvalidemail")]
        public async Task<IActionResult> IsValidEmail(string email)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.IsValidMail(email);

                if (status == 0)
                {
                    return BadRequest(new { IsSuccess = false, message = "Invalid payload" });
                }
                else if (status == 404)
                {
                    return BadRequest(new { IsSuccess = false, message = "User not found" });
                }

                return Ok(new { IsSuccess = true, message = "Email is valid" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPut]
        [Route("insertvalidationcode")]
        public async Task<IActionResult> InsertValidationCode(string email)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, messageResult) = await _authService.InsertValidationCode(email);

                if (status == 0)
                {
                    return BadRequest(new { IsSuccess = false, message = "Invalid payload" });
                }
                else if (status == 404)
                {
                    return BadRequest(new { IsSuccess = false, message = "User not found" });
                }
                //also return the code
                return Ok(new { IsSuccess = true, message = messageResult });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        [Route("deletevalidationcode")]
        public async Task<IActionResult> DeleteValidationCode(string email)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.DeleteValidationCode(email);

                if (status == 0)
                {
                    return BadRequest(new { IsSuccess = false, message = "Invalid payload" });
                }
                else if (status == 404)
                {
                    return BadRequest(new { IsSuccess = false, message = "User not found" });
                }

                return Ok(new { IsSuccess = true, message = message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("validatecode")]
        public async Task<IActionResult> ValidateCode(string email, string code)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.ValidateCode(email, code);

                if (status == 0)
                {
                    return BadRequest(new { IsSuccess = false, message = "Invalid payload" });
                }
                else if (status == 404)
                {
                    return BadRequest(new { IsSuccess = false, message = "User not found" });
                }

                return Ok(new { IsSuccess = true, message = "Code is valid" });
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
