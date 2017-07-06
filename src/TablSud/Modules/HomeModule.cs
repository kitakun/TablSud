using Nancy;
using TablSud.Core.Data.Interfaces;
using TablSud.Core.Domain.Auth;
using TablSud.Core.Domain.Court;
using TablSud.Services.Auth;
using TablSud.Web.Models.Convictions;

namespace TablSud.Web.Modules
{
    /// <summary>
    /// Main non-auth module with index-page
    /// </summary>
    public sealed class HomeModule : NancyModule
    {
        public HomeModule(IRepository<ConvictionsItem> convectionRepository)
        {
            //index
            Get("/", parameters =>
            {
                this.PlaceUserInfo();
                if (((TsUser) ViewBag.User).IsAuthenticated)
                {
                    return View["index_logged", new IndexConvictions(convectionRepository.Page(), convectionRepository.Size())];
                }
                return View["index", new IndexConvictions(convectionRepository.Page(), convectionRepository.Size())];
            });
            //paging
            Get("/from={from}", parameters =>
            {
                this.PlaceUserInfo();

                int fromIndex = 0;
                dynamic from = parameters.from;
                if (from != null && from.HasValue is bool && (bool)from.HasValue)
                {
                    int.TryParse(from.Value.ToString(), out fromIndex);
                }
                return View["index", new IndexConvictions(convectionRepository.Page(fromIndex), convectionRepository.Size())];
            });
            Get("/fromlogged={from}", parameters =>
            {
                this.PlaceUserInfo();

                int fromIndex = 0;
                dynamic from = parameters.from;
                if (from != null && from.HasValue is bool && (bool)from.HasValue)
                {
                    int.TryParse(from.Value.ToString(), out fromIndex);
                }
                return View["index_logged", new IndexConvictions(convectionRepository.Page(fromIndex), convectionRepository.Size())];
            });
        }
    }
}
