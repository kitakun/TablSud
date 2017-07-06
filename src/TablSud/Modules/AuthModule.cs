using System;
using System.Linq;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;
using TablSud.Core.Data.Interfaces;
using TablSud.Core.Domain.Auth;
using TablSud.Core.Extensions;
using TablSud.Services.Auth;
using TablSud.Services.Security;
using TablSud.Web.Models.Auth;

namespace TablSud.Web.Modules
{
    /// <summary>
    /// Auth module with login/logout logic
    /// </summary>
    public sealed class AuthModule : NancyModule
    {
        private const string InvalidLogPassErrQuerry = "invalid";

        public AuthModule(IHasher hash, IRepository<TsUser> userRepo)
        {
            Get("/login", parameters =>
            {
                LoginViewModel loginModel = new LoginViewModel();
                dynamic err = Request.Query.err;
                if ((bool) err.HasValue)
                {
                    dynamic errText = err.Value.ToString();
                    if (errText == InvalidLogPassErrQuerry)
                    {
                        loginModel.Error = "Неправильно указан логин и/или пароль";
                    }
                }
                return View["Login", loginModel];
            });

            Get("/logout", parameters =>
            {
                Session["uid"] = null;
                return this.Logout("/");
            });

            //submit login action
            Post("/login", parameters => {
                
                LoginModel loginModel = this.Bind<LoginModel>();

                TsUser userByLogin = userRepo.Filter(x => x.Login == loginModel.Username).FirstOrDefault();
                if (userByLogin != null)
                {
                    byte[] usrSalt = Convert.FromBase64String(userByLogin.PasswordSalt);
                    string hashedPass = hash.HashPassword(loginModel.Password, usrSalt);
                    if (hashedPass == userByLogin.PasswordHash)
                    {
                        Guid userId = userByLogin.Id.AsGuid();
                        Session["uid"] = userId.ToString();
                        return this.LoginAndRedirect(userId);
                    }
                }

                return Response.AsRedirect($"/login?err={InvalidLogPassErrQuerry}");
            });

            //submit register action
            Post("/register", parameters =>
            {
                LoginModel loginModel = this.Bind<LoginModel>();

                return this.ToRoot();
            });
        }
    }
}
;