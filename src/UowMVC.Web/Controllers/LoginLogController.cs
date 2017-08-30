using UowMVC.Models;
using UowMVC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace UowMVC.Web.Controllers
{
    [RoleAuthorize]
    public class LoginLogController : BaseController
    {
        private readonly ILoginLogService _loginLogService;
        public LoginLogController(ILoginLogService loginLogService)
        {
            this._loginLogService = loginLogService;
        }
        public ActionResult Index(string key, int result = -1, int pageIndex = 1)
        {
            var limit = DefaultPageSize;
            var offset = (pageIndex - 1) * limit;
            var count = 0;
            var items = _loginLogService.Query(key, offset, limit, out count, result);
            var model = new PagedList<LoginLogViewModel>(items, pageIndex, limit, count);
            ViewBag.key = key;
            ViewBag.result = result;
            return View(model);
        }
    }
}