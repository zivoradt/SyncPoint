using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SyncPointBack.Auth.Users;
using System;
using System.Collections.Generic;

namespace SyncPointBackTest.MockingProvider
{
    public static class UserManagerFactory
    {
        public static UserManager<ApplicationUser> CreateUserManager()
        {
            var fakeUserStore = A.Fake<IUserStore<ApplicationUser>>();
            var fakePasswordHasher = A.Fake<IPasswordHasher<ApplicationUser>>();
            var fakeUserValidators = new List<IUserValidator<ApplicationUser>> { A.Fake<IUserValidator<ApplicationUser>>() };
            var fakePasswordValidators = new List<IPasswordValidator<ApplicationUser>> { A.Fake<IPasswordValidator<ApplicationUser>>() };
            var fakeKeyNormalizer = A.Fake<ILookupNormalizer>();
            var fakeErrors = A.Fake<IdentityErrorDescriber>();
            var fakeServices = A.Fake<IServiceProvider>();
            var fakeLogger = A.Fake<ILogger<UserManager<ApplicationUser>>>();

            var userManager = A.Fake<UserManager<ApplicationUser>>(options => options.Wrapping(
                new UserManager<ApplicationUser>(
                    fakeUserStore,
                    null, // Pass null for IUserPasswordStore<ApplicationUser> if not needed
                    fakePasswordHasher,
                    fakeUserValidators,
                    fakePasswordValidators,
                    fakeKeyNormalizer,
                    fakeErrors,
                    fakeServices,
                    fakeLogger
                )));

            return userManager;
        }
    }
}