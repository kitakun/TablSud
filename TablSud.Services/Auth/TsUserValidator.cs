using System;
using System.Linq;
using System.Security.Claims;
using Nancy;
using Nancy.Authentication.Forms;
using TablSud.Core.Data.Interfaces;
using TablSud.Core.Domain.Auth;
using TablSud.Core.Extensions;

namespace TablSud.Services.Auth
{
    /// <summary>
    /// Provides a mapping between forms auth guid identifiers and real usernames
    /// </summary>
    public class TsUserMapper : IUserMapper
    {
        private readonly IRepository<TsUser> _userRepo;

        /// <summary>
        /// ctor
        /// </summary>
        public TsUserMapper(IRepository<TsUser> userRepo)
        {
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }

        //nancy method for principal creation from identificator
        public ClaimsPrincipal GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            TsUser userByLogin = _userRepo.Filter(x => x.Id == identifier.AsObjectId()).FirstOrDefault();

            if (userByLogin == null)
                return null;

            context.CurrentUser = new TsUserPrincipal(userByLogin);
            
            return context.CurrentUser;
            
        }
    }
}
