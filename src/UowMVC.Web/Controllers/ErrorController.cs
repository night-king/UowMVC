using UowMVC.Web.Models;
using UowMVC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UowMVC.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogService _logService;
        public ErrorController(ILogService logService)
        {
            this._logService = logService;
        }
        public ActionResult Index()
        {
            var model = ViewData.Model as ErrorModel;
            return View(model);

        }
        public ActionResult Error401()
        {
            var model = ViewData.Model as ErrorModel;
            return View(model);

        }
        [HttpPost]
        public ActionResult Report(ErrorModel model)
        {
            var id = model.Id;
            _logService.Report(id);
            return RedirectToAction("Success", new { id = id });
        }

        public ActionResult Success(string id)
        {
            ViewBag.id = id;
            return View();

        }
    }
}