using UowMVC.Repository;
using Autofac;
using Autofac.Integration.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace UowMVC.Web
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        private new string[] Roles { get; set; }

        /// <summary>
        /// 重写未验证通过的请求
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var area = filterContext.RouteData.DataTokens["area"];
            if (filterContext.HttpContext.User.Identity.IsAuthenticated == false)
            {
                ///跳转到指定的身份验证页面
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                    { "area", area},
                    { "controller", "Account" },
                    { "action", "Login" },
                    { "ReturnUrl", filterContext.HttpContext.Request.RawUrl }
                    });
            }
            else
            {
                ///跳转到401
                filterContext.Result = new RedirectResult("/401.html");
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var context = filterContext.HttpContext;
            var identity = context.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                var claims = identity.FindAll(ClaimTypes.Role);
                if (claims != null)
                {
                    var ctx = context.GetOwinContext().GetAutofacLifetimeScope().Resolve<DefaultDataContext>();
                    {
                        var isSuperAdmin = ctx.Users.Any(x => x.UserName == identity.Name && x.IsSuperAdmin == true);
                        if (isSuperAdmin)
                        {
                            return;
                        }
                        var url = filterContext.RequestContext.HttpContext.Request.Path.ToLower();
                        var roles = ctx.RolePermissions.Where(x => x.Menu.URL.ToLower() == url || x.Menu.RelevantURL.ToLower().Contains(url)).Select(x => x.Role);
                        this.Roles = roles.Select(x => x.Name).ToArray();
                    }
                }
            }
            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated == false)
            {
                return false;
            }

            var requestContext = httpContext.Request.RequestContext;

            if (Roles == null || Roles.Length == 0)
            {
                return false;
            }

            if (Roles.Any(x => httpContext.User.IsInRole(x)))
            {
                return true;
            }

            return false;
        }
    }
}