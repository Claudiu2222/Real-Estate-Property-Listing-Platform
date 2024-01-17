using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Contracts
{
    public interface IPredictPriceService
    {
        public Task<ApiResponsePredictPrice> PredictPriceAsync(PredictPriceViewModel model);
    }
}
