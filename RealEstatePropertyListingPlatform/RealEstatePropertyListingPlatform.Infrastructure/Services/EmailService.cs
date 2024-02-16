using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RealEstatePropertyListingPlatform.Application.Contracts;
using RealEstatePropertyListingPlatform.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace RealEstatePropertyListingPlatform.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailSettings> options;
        private readonly ILogger<EmailService> logger;

        public EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger)
        {
            this.options = options;
            this.logger = logger;
        }
        public async Task<bool> SendEmailAsync(Mail email)
        {
            try
            {
                var client = new SendGridClient(options.Value.ApiKey);

                var subject = email.Subject;
                var body = email.Body;
                var replyTo = new EmailAddress(email.ReplyTo);
                var to = new EmailAddress(email.To);

                var from = new EmailAddress
                {
                    Email = options.Value.FromAddress,
                    Name = options.Value.FromName
                };


                var message = MailHelper.CreateSingleEmail(from, to, subject, body, body);
                message.ReplyTo = replyTo;

                var response = await client.SendEmailAsync(message);

                logger.LogInformation(response.StatusCode.ToString());
                logger.LogInformation(response.Body.ReadAsStringAsync().Result);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                logger.LogError($"Eroare la trimiterea e-mailului: {ex.Message}");
                return false;
            }
        }
    }
}
