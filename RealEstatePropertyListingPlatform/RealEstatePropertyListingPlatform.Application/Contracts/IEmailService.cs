using RealEstatePropertyListingPlatform.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePropertyListingPlatform.Application.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Mail email);
    }
}
