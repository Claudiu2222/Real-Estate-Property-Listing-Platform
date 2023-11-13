using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandResponse:BaseResponse
    {
        public UpdateUserCommandResponse() : base()
        {
        }

        public UserDto User { get; set; }

    }
}
