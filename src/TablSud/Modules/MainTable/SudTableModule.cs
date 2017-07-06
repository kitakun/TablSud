using MongoDB.Bson;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using TablSud.Core.Data.Interfaces;
using TablSud.Core.Domain.Court;
using TablSud.Services.Auth;

namespace TablSud.Web.Modules.MainTable
{
    /// <summary>
    /// CRUD of cort data
    /// </summary>
    public sealed class SudTableModule : NancyModule
    {
        public SudTableModule(IRepository<ConvictionsItem> convectionRepository):base("SudTable")
        {
            this.Secure();

            //get creation page
            Get("/NewConv", parameters => View["Convictions/CreateConviction"]);

            //create cort data
            Post("/NewConv", args =>
            {
                ConvictionsItem model = this.Bind<ConvictionsItem>();
                model.Sides = this.Bind<ModelConvictionSides>();
                model.Progress = this.Bind<ModelConvictionProgress>();

                convectionRepository.Insert(model);

                return this.ToRoot();
            });

            Get("/Delete/{id}", parameters =>
            {
                dynamic objId = parameters.id;
                ObjectId mongoId = ObjectId.Parse(objId.ToString());
                convectionRepository.Delete(mongoId);
                return this.ToRoot();
            });

            Get("/DeleteAll", p =>
            {
                convectionRepository.DeletaAll();
                return this.ToRoot();
            });
        }
    }
}
