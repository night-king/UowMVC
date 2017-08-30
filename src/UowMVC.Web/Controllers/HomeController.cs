using UowMVC.Web.Helpers;
using UowMVC.Web.Models;
using UowMVC.SDK;
using UowMVC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UowMVC.Domain;
using System.IO;
using System.Xml.Serialization;
using UowMVC.Models;

namespace UowMVC.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IItemCountService _itemCountService;
        public HomeController(IItemCountService itemCountService)
        {
            this._itemCountService = itemCountService;
        }
        public ActionResult Index()
        {
            var model = _itemCountService.GetHome();
            return View(model);
        }
    }
}