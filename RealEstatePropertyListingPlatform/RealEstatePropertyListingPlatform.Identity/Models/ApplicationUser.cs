﻿using Microsoft.AspNetCore.Identity;

namespace RealEstatePropertyListingPlatform.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? ValidationCode { get; set; }


    }
}
