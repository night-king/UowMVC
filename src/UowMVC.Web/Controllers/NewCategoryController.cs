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
    public class NewCategoryController : BaseController
    {
        readonly INewsCategoryService _newsCategoryService;
        public NewCategoryController(INewsCategoryService newsCategoryService)
        {
            _newsCategoryService = newsCategoryService;
        }

        public ActionResult Index()
        {
            var items = _newsCategoryService.GetAll();
            return View(items);
        }

        public ActionResult New()
        {
            var mdoel = new NewsCategoryViewModel
            {
            };
            return View(mdoel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult New(NewsCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _newsCategoryService.Add(model);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }

        public ActionResult Edit(string id)
        {
            var mdoel = _newsCategoryService.GetById(id);
            return View(mdoel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(NewsCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _newsCategoryService.Update(model);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }

        public ActionResult Delete(string id)
        {
            var mdoel = _newsCategoryService.GetById(id);
            return View(mdoel);
        }

        [HttpPost]
        public ActionResult Delete(NewsCategoryViewModel model)
        {
            var result = _newsCategoryService.Delete(model.Id);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });

        }
    }
}