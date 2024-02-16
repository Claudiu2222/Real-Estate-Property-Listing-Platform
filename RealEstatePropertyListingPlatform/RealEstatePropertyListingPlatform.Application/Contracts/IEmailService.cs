using RealEstatePropertyListingPlatform.Application.Models;

namespace RealEstatePropertyListingPlatform.Application.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Mail email);
    }
}
