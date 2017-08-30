using UowMVC.Web.Helpers;
using UowMVC.Web.Models;
using UowMVC.Domain;
using UowMVC.Models;
using UowMVC.Repository;
using UowMVC.Service.Interfaces;
using Autofac;
using Autofac.Integration.Owin;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace UowMVC.Web.Controllers
{
    public class BaseController : Controller
    {

        public int DefaultPageSize
        {
            get
            {
                return 10;
            }
        }
        private IUnitOfWork _uow;
        public IUnitOfWork uow
        {
            get
            {
                if (_uow == null)
                {
                    _uow = new UnitOfWork(HttpContext.GetOwinContext().GetAutofacLifetimeScope().Resolve<DefaultDataContext>());
                }
                return _uow;
            }
        }
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private string TryGetClaimValue(string claimType)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return null;
            }

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(claimType);
            return claim == null ? null : claim.Value;
        }

        protected string UserId
        {
            get
            {
                return TryGetClaimValue(ClaimTypes.NameIdentifier);
            }
        }
        protected string UserName
        {
            get
            {
                return User.Identity.Name;
            }
        }
        public IEnumerable<string> Roles
        {
            get
            {
                var identity = User.Identity as ClaimsIdentity;
                return identity.FindAll(ClaimTypes.Role).Select(x => x.Value);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
                if (_uow != null)
                {
                    _uow.Dispose();
                    _uow = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}