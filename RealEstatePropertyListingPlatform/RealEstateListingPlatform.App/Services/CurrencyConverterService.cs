using RealEstateListingPlatform.App.Contracts;
using System.Net.Http.Json;

namespace RealEstateListingPlatform.App.Services
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        private readonly HttpClient _httpClient;
        private readonly string apiKey;

        public CurrencyConverterService(HttpClient httpClient)
        {
            apiKey = "c457253b9dfecfc38634a6c5";
            _httpClient = httpClient;
        }

        public async Task<double> ConvertToUSD(double euroPrice)
        {
            string apiEndpoint = $"https://v6.exchangerate-api.com/v6/c457253b9dfecfc38634a6c5/latest/EUR";

            HttpResponseMessage result = await _httpClient.GetAsync(apiEndpoint);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<ExchangeRates>();

                if (response != null && response.Conversion_Rates != null &&
                    response.Conversion_Rates.TryGetValue("USD", out double usdRate))
                {
                    double usdPrice = euroPrice * usdRate;
                    return usdPrice;
                }
            }

            // În caz de eroare sau date lipsă, întoarce o valoare semnificativă sau aruncă o excepție
            return -1;
        }

        public async Task<double> ConvertToRON(double euroPrice)
        {
            string apiEndpoint = $"https://v6.exchangerate-api.com/v6/c457253b9dfecfc38634a6c5/latest/EUR";

            HttpResponseMessage result = await _httpClient.GetAsync(apiEndpoint);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<ExchangeRates>();

                if (response != null && response.Conversion_Rates != null &&
                                       response.Conversion_Rates.TryGetValue("RON", out double ronRate))
                {
                    double ronPrice = euroPrice * ronRate;
                    return ronPrice;
                }
            }
            // În caz de eroare sau date lipsă, întoarce o valoare semnificativă
            return -1;
        }

        private class ExchangeRates
        {
            public Dictionary<string, double> Conversion_Rates { get; set; }
        }
    }
}
