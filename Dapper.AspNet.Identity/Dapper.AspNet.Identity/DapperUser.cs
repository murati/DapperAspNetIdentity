using Microsoft.AspNet.Identity;

namespace Dapper.AspNet.Identity
{
    public class DapperUser : IUser<string>
    {
        public DapperUser() { }
        public int UserCode { get; set; }
        public string Id
        {
            get { return UserCode.ToString(); }
        }
        public string Password { get; set; }
        public string UserName
        {
            get;
            set;
        }
        public string SecurityStamp { get; set; }
        public string PasswordHash { get; set; }

    }
}