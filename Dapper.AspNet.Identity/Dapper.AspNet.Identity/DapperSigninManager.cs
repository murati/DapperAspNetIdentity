using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;

namespace Dapper.AspNet.Identity
{
    public class DapperSigninManager : SignInManager<DapperUser, string>
    {
        public DapperSigninManager(DapperUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }
        public static DapperSigninManager Create(IdentityFactoryOptions<DapperSigninManager> options, IOwinContext context)
        {
            return new DapperSigninManager(context.GetUserManager<DapperUserManager>(), context.Authentication);
        }

        public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {

            DapperUser user = await UserManager.FindByNameAsync(userName);
            bool passMatch = await UserManager.CheckPasswordAsync(user, password);
            if (passMatch)
            {
                var userIdentity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, userIdentity);
                return SignInStatus.Success;
            }
            return SignInStatus.Failure;
        }
    }
}