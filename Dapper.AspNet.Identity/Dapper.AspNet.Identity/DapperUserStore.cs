using Dapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
namespace Dapper.AspNet.Identity
{
    public class OneMoreUserStore : IUserStore<DapperUser>,
        IUserLoginStore<DapperUser>,
        IUserPasswordStore<DapperUser>,
        IQueryableUserStore<DapperUser>,
        IUserTwoFactorStore<DapperUser, string>,
        IUserLockoutStore<DapperUser, string>,
        IUserEmailStore<DapperUser>,
        IUserPhoneNumberStore<DapperUser>,
        IDisposable
    {
        //private CustomDbContext database;
        public readonly string connStr = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
        public IDbConnection database;
        public OneMoreUserStore()
        {
            this.database = new SqlConnection(connStr);
        }

        public void Dispose()
        {
            this.database.Dispose();
        }

        public Task CreateAsync(DapperUser user)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task UpdateAsync(DapperUser user)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task DeleteAsync(DapperUser user)
        {
            // TODO
            throw new NotImplementedException();
        }
        /// <summary>
        /// This method is automatically called by Microsoft DLL
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<DapperUser> IUserStore<DapperUser, string>.FindByIdAsync(string userId)
        {
            DapperUser user = this.database.Query<DapperUser>("SELECT UserCode,DisplayName as 'UserName',Password FROM Users WHERE UserCode = @UserCode", new { UserCode = userId }).FirstOrDefault();
            return Task.FromResult<DapperUser>(user);
        }
        /// <summary>
        /// 'AS' usage within the query points to DapperUser class attribute map
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<DapperUser> IUserStore<DapperUser, string>.FindByNameAsync(string userName)
        {
            DapperUser user = this.database.Query<DapperUser>("SELECT UserCode,DisplayName as 'UserName',Password FROM Users WHERE DisplayName = @DisplayName", new { DisplayName = userName }).FirstOrDefault();
            return Task.FromResult<DapperUser>(user);
        }

        #region IUserLockoutStore
        public Task<int> GetAccessFailedCountAsync(DapperUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetLockoutEnabledAsync(DapperUser user)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(DapperUser user)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(DapperUser user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(DapperUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(DapperUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(DapperUser user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }
        #endregion

        public Task SetPasswordHashAsync(DapperUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(passwordHash);
        }
        public Task<string> GetPasswordHashAsync(DapperUser user)
        {
            return Task.FromResult<string>(user.Password);
            //var identityUser = ToIdentityUser(user);
            //var task = userStore.GetPasswordHashAsync(identityUser);
            //SetApplicationUser(user, identityUser);
            //return task;

        }
        public Task<bool> HasPasswordAsync(DapperUser user)
        {
            return Task.FromResult<bool>(!String.IsNullOrEmpty(user.Password));
            //var identityUser = ToIdentityUser(user);
            //var task = userStore.HasPasswordAsync(identityUser);
            //SetApplicationUser(user, identityUser);
            //return task;
        }

        public Task AddLoginAsync(DapperUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        Task<DapperUser> IUserLoginStore<DapperUser, string>.FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(DapperUser user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(DapperUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task AddClaimAsync(DapperUser user, System.Security.Claims.Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(DapperUser user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(DapperUser user, System.Security.Claims.Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSecurityStampAsync(DapperUser user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(Guid.NewGuid().ToString());
        }

        public Task SetSecurityStampAsync(DapperUser user, string stamp)
        {
            if (user == null) throw new ArgumentNullException("user");
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        IQueryable<DapperUser> IQueryableUserStore<DapperUser, string>.Users
        {
            get { throw new NotImplementedException(); }
        }

        public Task<bool> GetTwoFactorEnabledAsync(DapperUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetTwoFactorEnabledAsync(DapperUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task<DapperUser> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(DapperUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(DapperUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(DapperUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(DapperUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPhoneNumberAsync(DapperUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(DapperUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberAsync(DapperUser user, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberConfirmedAsync(DapperUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }
    }
    public class OneMorePasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == HashPassword(providedPassword))
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
    }
}