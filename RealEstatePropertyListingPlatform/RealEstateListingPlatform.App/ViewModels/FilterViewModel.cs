using RealEstatePropertyListingPlatform.Domain.Enums;

namespace RealEstateListingPlatform.App.ViewModels
{

    public class FilterViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        private decimal? _priceLowerBound;
        public decimal? PriceLowerBound
        {
            get => _priceLowerBound;
            set

            {   // check is Price Upper bound is null
                if (PriceUpperBound == null || value == null)
                {
                    _priceLowerBound = value;
                }

                else if (value <= PriceUpperBound)
                {
                    _priceLowerBound = value;
                }
                else// Upper bound is not null and lower bound is greater than upper bound
                {
                    _priceLowerBound = PriceUpperBound;
                }
            }
        }

        private decimal? _priceUpperBound;
        public decimal? PriceUpperBound
        {
            get => _priceUpperBound;
            set
            {
                if (PriceLowerBound == null || value == null)
                {
                    _priceUpperBound = value;
                }
                else if (value >= PriceLowerBound)
                {
                    _priceUpperBound = value;
                }
                else
                {
                    _priceUpperBound = PriceLowerBound;
                }
            }
        }

        public Currency PriceCurrency { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public int PropertyType { get; set; }

        private int? _squareFeetLowerBound;
        public int? SquareFeetLowerBound
        {
            get => _squareFeetLowerBound;
            set
            {
                if (SquareFeetUpperBound == null || value == null)
                {
                    _squareFeetLowerBound = value;
                }
                else if (value <= SquareFeetUpperBound)
                {
                    _squareFeetLowerBound = value;
                }
                else
                {
                    _squareFeetLowerBound = SquareFeetUpperBound;
                }
            }
        }

        private int? _squareFeetUpperBound;
        public int? SquareFeetUpperBound
        {
            get => _squareFeetUpperBound;
            set
            {
                if (SquareFeetLowerBound == null || value == null)
                {
                    _squareFeetUpperBound = value;
                }
                else if (value >= SquareFeetLowerBound)
                {
                    _squareFeetUpperBound = value;
                }
                else
                {
                    _squareFeetUpperBound = SquareFeetLowerBound;
                }
            }
        }

        public bool? ForRent { get; set; }
        public string? ContainsInTitle { get; set; }
    }


}
