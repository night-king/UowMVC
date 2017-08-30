using UowMVC.Web.SignalR;
using UowMVC.Repository;
using UowMVC.Repository.Migrations;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(UowMVC.Web.Startup))]
namespace UowMVC.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseAutofacMiddleware(AutofacConfig.Container);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DefaultDataContext, Configuration>());
#if (DEBUG)
            GlobalHost.HubPipeline.AddModule(new LoggingPipelineModule());
#endif
            GlobalHost.HubPipeline.AddModule(new ErrorHandlingPipelineModule());
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
