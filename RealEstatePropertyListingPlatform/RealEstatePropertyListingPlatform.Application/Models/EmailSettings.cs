using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePropertyListingPlatform.Application.Models
{
    public class EmailSettings
    {
        public string ApiKey { get; init; } = default!;
        public string FromAddress { get; init; } = default!;
        public string FromName { get; init; } = default!;
    }
}
