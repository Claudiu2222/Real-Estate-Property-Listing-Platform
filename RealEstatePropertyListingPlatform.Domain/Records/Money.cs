using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePropertyListingPlatform.Domain.Records
{
    public record Money(string Currency, decimal Amount);
}
