using System;
using System.Linq;
using TablSud.Core.Data.Interfaces;
using TablSud.Core.Domain.Auth;
using TablSud.Services;
using TablSud.Services.Security;
using TablSud.Web.Models.Auth;

namespace TablSud.Web.Helpers
{
    public class AdminAccountCreation
    {
        /// <summary>
        /// Create admin account in db
        /// </summary>
        public static void Run()
        {
            LoginModel adminModel = new LoginModel
            {
                Username = "admin",
                Password = "mGHt3T"
            };

            IRepository<TsUser> userRepo = ContainerHolder.Resolve<IRepository<TsUser>>();
            TsUser admin = userRepo.Filter(x => x.Login == adminModel.Username).FirstOrDefault();
            if (admin == null)
            {
                TsUser adminUser = new TsUser
                {
                    Login = adminModel.Username
                };
                IHasher hasher = ContainerHolder.Resolve<IHasher>();
                byte[] rawSalt = hasher.GenerateSalt();
                adminUser.PasswordSalt = Convert.ToBase64String(rawSalt);
                adminUser.PasswordHash = hasher.HashPassword(adminModel.Password, rawSalt);
                userRepo.Insert(adminUser);
            }
        }
    }
}
