using System.Security.Claims;
using System.Security.Principal;
using TablSud.Core.Domain.Auth;

namespace TablSud.Services.Auth
{
    /// <summary>
    /// Custom principal with user link in it
    /// </summary>
    public class TsUserPrincipal : ClaimsPrincipal
    {
        public TsUser TsUser { get; }
        public string UserName => TsUser.Login;

        public TsUserPrincipal(TsUser user):base(new GenericIdentity(user.Login, "tsauth"))
        {
            TsUser = user;
        }

    }
}
