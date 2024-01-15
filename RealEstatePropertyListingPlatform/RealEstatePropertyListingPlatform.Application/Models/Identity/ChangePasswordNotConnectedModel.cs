using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePropertyListingPlatform.Application.Models.Identity
{
    public class ChangePasswordNotConnectedModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
