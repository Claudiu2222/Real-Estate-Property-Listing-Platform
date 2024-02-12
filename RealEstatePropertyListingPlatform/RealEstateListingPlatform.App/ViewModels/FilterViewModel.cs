using RealEstatePropertyListingPlatform.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace RealEstateListingPlatform.App.ViewModels
{

    public class FilterViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        private decimal _priceLowerBound;
        public decimal PriceLowerBound
        {
            get => _priceLowerBound;
            set => _priceLowerBound = Math.Min(value, PriceUpperBound);
        }

        private decimal _priceUpperBound;
        public decimal PriceUpperBound
        {
            get => _priceUpperBound;
            set => _priceUpperBound = Math.Max(value, PriceLowerBound);
        }

        public Currency PriceCurrency { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public int PropertyType { get; set; }

        private int _squareFeetLowerBound;
        public int SquareFeetLowerBound
        {
            get => _squareFeetLowerBound;
            set => _squareFeetLowerBound = Math.Min(value, SquareFeetUpperBound);
        }

        private int _squareFeetUpperBound;
        public int SquareFeetUpperBound
        {
            get => _squareFeetUpperBound;
            set => _squareFeetUpperBound = Math.Max(value, SquareFeetLowerBound);
        }

        public bool? ForRent { get; set; }
        public string? ContainsInTitle { get; set; }
    }


}
