using RealEstatePropertyListingPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandResponse : BaseResponse
    {
        public CreatePropertyCommandResponse() : base()
        {

        }

        public PropertyDto Property { get; set; }
    }
}
