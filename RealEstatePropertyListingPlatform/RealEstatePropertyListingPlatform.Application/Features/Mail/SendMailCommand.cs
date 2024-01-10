using MediatR;
using RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.CreateListing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePropertyListingPlatform.Application.Features.Mail
{
    public class SendMailCommand : IRequest<SendMailCommandResponse>
    {
    }
}
