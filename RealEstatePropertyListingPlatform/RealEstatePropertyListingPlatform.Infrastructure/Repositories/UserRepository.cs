﻿using Microsoft.EntityFrameworkCore;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(RealEstatePropertyListingPlatformContext context) : base(context)
        {
        }

        public async Task<Result<User>> FindByEmailAsync (string email)
        {
            var result = await context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();

            if (result == null)
            {
                return Result<User>.Failure($"Entity with email {email} not found");
            }
            return Result<User>.Success(result);
        }

        public async Task<Result<User>> FindByPhoneNumberAsync(string phoneNumber)
        {
            var result = await context.Users.Where(x => x.PhoneNumber == phoneNumber).FirstOrDefaultAsync();

            if (result == null)
            {
                return Result<User>.Failure($"Entity with email {phoneNumber} not found");
            }
            return Result<User>.Success(result);
        }

    }

}
