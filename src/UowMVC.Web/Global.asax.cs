using UowMVC.Web.Controllers;
using UowMVC.Web.Models;
using UowMVC.Repository;
using UowMVC.SDK;
using UowMVC.Service.Interfaces;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UowMVC.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            WebConfig.Register();
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("log4netconfig.xml"));
        }
        protected void Application_End()
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var httpContext = ((MvcApplication)sender).Context;
            var url = Request.Url.AbsoluteUri.ToLower();

            var username = httpContext.User != null && httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : "";
            Exception lastError = Server.GetLastError();
            if (lastError != null)
            {
                Exception ex = lastError.GetBaseException();
                var statusCode = 500;
                var httpException = ex as HttpException;
                if (httpException != null)
                {
                    statusCode = httpException.GetHttpCode();
                }
                var id = RandomIdGenerator.NewId();
                string ip = "";
                if (Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null)
                {
                    ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
                }
                else
                {
                    ip = Request.ServerVariables.Get("Remote_Addr").ToString().Trim();
                }
                var browser = Request.Browser.Browser.ToString();
                var browser_version = Request.Browser.MajorVersion.ToString();
                var browser_platform = Request.Browser.Platform.ToString();

                var client = string.Format("{0}-{1}-{2}", browser, browser_version, browser_platform);
                //创建路径 

                LogWriter.Default.WriteError(new LogContent(id, url, ex.Message, ip, username, "Application_Error", client, statusCode.ToString()), ex);

                //一定要调用Server.ClearError()否则会触发错误详情页（就是黄页）
                Server.ClearError();


                var routeData = new RouteData();
                routeData.Values["controller"] = "Error";
                routeData.Values["action"] = "Index";
                var controller = new ErrorController(Request.GetOwinContext().GetAutofacLifetimeScope().Resolve<ILogService>());
                controller.ViewData.Model = new ErrorModel
                {
                    Id = id,
                    Browser = new ErrorBrowserModel
                    {
                        Browser = browser,
                        MajorVersion = browser_version,
                        Platform = browser_platform
                    },
                    Exception = new ErrorExceptionModel
                    {
                        Message = ex.Message,
                        Source = ex.Source == null ? "" : ex.Source,
                        TargetSite = ex.TargetSite == null ? "" : ex.TargetSite.ToString(),
                        StackTrace = ex.StackTrace == null ? "" : ex.StackTrace,
                    },
                    StatusCode = statusCode,
                    IP = ip,
                    Url = Request.Url.AbsoluteUri
                };
                ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
            }
        }
    }
}
