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
    public class SysLogController : BaseController
    {
        private readonly ILogService _logService;
        public SysLogController(ILogService logService)
        {
            this._logService = logService;
        }
        public ActionResult Index(string key, string level = null, int pageIndex = 1)
        {
            var limit = DefaultPageSize;
            var offset = (pageIndex - 1) * limit;
            var count = 0;
            var items = _logService.Query(key, offset, limit, out count, level);
            var model = new PagedList<LogViewModel>(items, pageIndex, limit, count);
            ViewBag.key = key;
            ViewBag.level = level;

            return View(model);
        }
        public ActionResult Detail(string id)
        {
            var model = _logService.GetById(id);
            return View(model);

        }
    }
}