using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Contracts
{
    public interface ISendMailService
    {
        Task<ApiResponseSendMail> SendMailAsync(SendMailViewModel sendMailViewModel);
    }
}
