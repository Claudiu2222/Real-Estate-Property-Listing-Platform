using Microsoft.ML.Data;

namespace RealEstatePropertyListingPlatform.API.Utility
{
    public class ModelOutput
    {
        [ColumnName("Score")]
        public float Pret { get; set; }
    }
}
