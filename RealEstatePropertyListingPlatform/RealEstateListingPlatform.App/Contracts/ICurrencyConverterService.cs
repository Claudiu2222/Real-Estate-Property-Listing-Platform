namespace RealEstateListingPlatform.App.Contracts
{
    public interface ICurrencyConverterService
    {
        public Task<double> ConvertToUSD(double euroPrice);

        public Task<double> ConvertToRON(double euroPrice);
    }
}
