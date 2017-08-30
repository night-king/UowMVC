using UowMVC.Web.Helpers;
using UowMVC.Models;
using UowMVC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UowMVC.Web.Controllers
{
    [RoleAuthorize]
    public class DictIndexController : Controller
    {
        private readonly IDictService _dictService;
        private readonly IDictIndexService _dictIndexService;

        public DictIndexController(IDictService dictService, IDictIndexService dictIndexService)
        {
            this._dictService = dictService;
            this._dictIndexService = dictIndexService;
        }

        public ActionResult New(string id)
        {
            var mdoel = new DictIndexViewModel
            {
                ParentID = id,
            };
            return View(mdoel);
        }

        [HttpPost]
        public ActionResult New(DictIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _dictIndexService.Add(model);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }

        public ActionResult Edit(string id)
        {
            var mdoel = _dictIndexService.GetById(id);
            return View(mdoel);
        }

        [HttpPost]
        public ActionResult Edit(DictIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _dictIndexService.Update(model);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }

        public ActionResult Delete(string id)
        {
            var mdoel = _dictIndexService.GetById(id);
            return View(mdoel);
        }

        [HttpPost]
        public ActionResult Delete(DictIndexViewModel model)
        {
            var result = _dictIndexService.Delete(model.Id);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });

        }

        public JsonResult getTree(string checkedId)
        {
            var nodes = _dictIndexService.GetAll().Select(x => new ZTree(x.Id, x.Name, x.ParentID));
            var tree = ZTreeHelper.ToJson(nodes, "/Dicts", checkedId);
            return Json(tree);
        }
    }
}