using System.Security.Principal;
using TablSud.Core.Domain.Auth;
using TablSud.Services.Auth;

namespace TablSud.Services.Extensions
{
    public static class PrincipalExtensions
    {
        /// <summary>
        /// Get TsUser from principal
        /// </summary>
        public static TsUser GetUser(this IPrincipal principal)
        {
            if (principal != null)
            {
                return principal as TsUser ?? ((TsUserPrincipal)principal).TsUser;
            }
            
            return null;
        }

        /// <summary>
        /// Get custom TsUserPrincila from base object
        /// </summary>
        public static TsUserPrincipal GetPrincipal(this IPrincipal principal)
        {
            return principal as TsUserPrincipal;
        }

    }
}
