using Microsoft.ML.Data;

namespace RealEstatePropertyListingPlatform.API.Utility
{
    public class ModelInput
    {
        [LoadColumn(0)] // 'Nr_Camere'
        public float Nr_Camere { get; set; }

        [LoadColumn(1)] // 'Suprafata'
        public float Suprafata { get; set; }

        [LoadColumn(2)] // 'Etaj'
        public float Etaj { get; set; }

        [LoadColumn(3)] // 'Total_Etaje'
        public float Total_Etaje { get; set; }

        [LoadColumn(4)] // 'City'
        public float City { get; set; }

        [LoadColumn(5)] // 'Pret'
        public float Pret { get; set; }
    }
}

