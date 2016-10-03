using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;

namespace Dapper.AspNet.Identity
{
    public class DapperUserManager : UserManager<DapperUser>
    {
        public DapperUserManager(IUserStore<DapperUser> store)
            : base(store)
        {
            PasswordHasher = new OneMorePasswordHasher();
        }
        public static DapperUserManager Create(IdentityFactoryOptions<DapperUserManager> options, IOwinContext context)
        {
            OneMoreUserStore store = new OneMoreUserStore();
            DapperUserManager manager = new DapperUserManager(store);
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<DapperUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
 
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =                    new DataProtectorTokenProvider<DapperUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}