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
    public class DictsController : BaseController
    {
        private readonly IDictService _dictService;
        private readonly IDictIndexService _dictIndexService;

        public DictsController(IDictService dictService, IDictIndexService dictIndexService)
        {
            this._dictService = dictService;
            this._dictIndexService = dictIndexService;
        }

        public ActionResult Index(string key, string fid, int pageIndex = 1)
        {
            var limit = DefaultPageSize;
            var offset = (pageIndex - 1) * limit;
            var count = 0;
            var items = _dictService.Query(key, offset, limit, out count, fid);
            var model = new PagedList<DictViewModel>(items, pageIndex, limit, count);
            ViewBag.key = key;
            ViewBag.fid = fid;
            return View(model);
        }

        public ActionResult New(string id)
        {
            var mdoel = new DictViewModel
            {
                IndexID = id,
            };
            return View(mdoel);
        }

        [HttpPost]
        public ActionResult New(DictViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _dictService.Add(model);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }
        public ActionResult Edit(string id)
        {
            var mdoel = _dictService.GetById(id);
            return View(mdoel);
        }

        [HttpPost]
        public ActionResult Edit(DictViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _dictService.Update(model);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }

        public ActionResult Delete(string id)
        {
            var mdoel = _dictService.GetById(id);
            return View(mdoel);
        }

        [HttpPost]
        public ActionResult Delete(DictViewModel model)
        {
            var result = _dictService.Delete(model.Id);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });

        }
        public ActionResult Detail(string id)
        {
            var mdoel = _dictService.GetById(id);
            return View(mdoel);
        }
    }
}