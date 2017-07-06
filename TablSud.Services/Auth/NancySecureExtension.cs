using System;
using System.Linq;
using Nancy;
using Nancy.Authentication.Forms;
using TablSud.Core.Data.Interfaces;
using TablSud.Core.Domain.Auth;
using TablSud.Core.Extensions;

namespace TablSud.Services.Auth
{
    public static class NancySecureExtension
    {
        public const int SmacLength = 32;   //via reflection
        /// <summary>
        /// Enable auth protection for module
        /// </summary>
        public static void Secure(this INancyModule currentModule)
        {
            FormsAuthenticationConfiguration authConfig = new FormsAuthenticationConfiguration
            {
                RedirectUrl = "~/login",
                UserMapper = ContainerHolder.Resolve<IUserMapper>()
            };
            FormsAuthentication.Enable(currentModule, authConfig);
        }

        /// <summary>
        /// Place TsUser in bag from cookie
        /// </summary>
        /// <param name="currentModule"></param>
        public static void PlaceUserInfo(this NancyModule currentModule)
        {
            object uidObj = currentModule.Session["uid"];
            if (uidObj != null)
            {
                IRepository<TsUser> repo = ContainerHolder.Resolve<IRepository<TsUser>>();
                Guid userGuid = Guid.Parse(uidObj.ToString());
                TsUser foundedUsr = repo.Filter(x => x.Id == userGuid.AsObjectId()).FirstOrDefault();
                currentModule.ViewBag.User = foundedUsr ?? TsUser.Empty;
            }
            else
            {
                currentModule.ViewBag.User = TsUser.Empty;
            }
        }

        /// <summary>
        /// Redirect current response to root url
        /// </summary>
        public static Response ToRoot(this NancyModule currentModule)
        {
            return currentModule.Response.AsRedirect("/");
        }
    }
}
