using Microsoft.AspNetCore.Mvc;
using RealEstatePropertyListingPlatform.Application.Contracts;
using RealEstatePropertyListingPlatform.Application.Models;

namespace RealEstatePropertyListingPlatform.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailController> _logger;

        public EmailController(IEmailService emailService
                       )
        {
            _emailService = emailService;
        }

        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> SendEmail(Mail mail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { IsSuccess = false, Message = "Invalid payload" });
            }

            var status = await _emailService.SendEmailAsync(mail);

            if (!status)
            {
                return BadRequest(new { IsSuccess = false, Message = "Email not sent, provide a valid address." });
            }

            return Ok(new { IsSuccess = true, Message = "Email sent successfully!" });
        }



    }
}
