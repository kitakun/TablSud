using System.Collections.Generic;
using Nancy;
using Nancy.Conventions;
using Nancy.TinyIoc;
using TablSud.Core.Configuration;
using TablSud.Core.Data.Interfaces;
using TablSud.Core.Data.Mongo;
using TablSud.Core.Domain.Court;
using TablSud.Services.Configuration;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Session;
using Nancy.ViewEngines.SuperSimpleViewEngine;
using TablSud.Core.Domain.Auth;
using TablSud.Services;
using TablSud.Services.Auth;
using TablSud.Services.Security;
using TablSud.Web.Helpers;
using TablSud.WebFramework.SshtmlAddons;

namespace TablSud.Web.Configurations
{
    /// <summary>
    /// Registration of dependencies in project
    /// </summary>
    public class TablSudBootstrap : DefaultNancyBootstrapper
    {
        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
            
            //pipelines.AfterRequest += ctx => { ctx.Response.Headers["X-Powered-By"] = "Nancy"; };
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            container.Place();
            
            CookieBasedSessions.Enable(pipelines);

            AdminAccountCreation.Run();
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            //auth
            container.Register(typeof(IHasher), typeof(Hasher));
            container.Register(typeof(IUserMapper), typeof(TsUserMapper));
            //Configuration
            container.Register(typeof(IDbConfigurator), new DbConfigurator());
            //DB
            container.Register(typeof(IConnectorFactory<>), typeof(MongoConnectorFactory));
            //sadly tinyioc can't proceed generic interfaces
            container.Register(typeof(IRepository<ConvictionsItem>), typeof(MongoRepository<ConvictionsItem>));
            container.Register(typeof(IRepository<TsUser>), typeof(MongoRepository<TsUser>));
            //SuperSimpleViewEngine
            //register custom sshtml addon
            /*container.Register<IEnumerable<ISuperSimpleViewEngineMatcher>>((c, p) => new List<ISuperSimpleViewEngineMatcher>{
                // This matcher provides support for @Translate. tokens
                new AdvancedIfTokenViewEngineMatcher()});*/
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);
            
            conventions.StaticContentsConventions.AddDirectory("css");
            conventions.StaticContentsConventions.AddDirectory("js");
        }
    }
}
