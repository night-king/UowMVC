using UowMVC.Models;
using UowMVC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using UowMVC.Web.Models;

namespace UowMVC.Web.Controllers
{
    [RoleAuthorize]
    public class NewsController : BaseController
    {
        readonly INewsCategoryService _newsCategoryService;
        readonly INewsService _newsService;

        public NewsController(INewsCategoryService newsCategoryService, INewsService newsService)
        {
            _newsCategoryService = newsCategoryService;
            _newsService = newsService;
        }

        public ActionResult Index(string key, string categoryId, int pageIndex = 1)
        {
            var limit = DefaultPageSize;
            var offset = (pageIndex - 1) * limit;
            var count = 0;
            var items = _newsService.Query(key, offset, limit, out count, 0, categoryId);
            var model = new PagedList<NewsViewModel>(items, pageIndex, limit, count);
            ViewBag.key = key;
            return View(model);
        }

        public ActionResult New()
        {
            ViewData["Categories"] = new SelectList(_newsCategoryService.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id,
            }), "value", "text");
            var mdoel = new NewsViewModel
            {
            };
            return View(mdoel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult New(NewsViewModel model)
        {
            ViewData["Categories"] = new SelectList(_newsCategoryService.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id,
            }), "value", "text");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _newsService.Add(model);
            return RedirectToAction("Index", "Result", new { state = result, returnUrl = "/News" });
        }

        public ActionResult Edit(string id)
        {
            var model = _newsService.GetById(id);
            ViewData["Categories"] = new SelectList(_newsCategoryService.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id,
            }), "value", "text", model.CategoryId);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(NewsViewModel model)
        {
            ViewData["Categories"] = new SelectList(_newsCategoryService.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id,
            }), "value", "text", model.CategoryId);

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _newsService.Update(model);
            return RedirectToAction("Index", "Result", new { state = result, returnUrl = "/News" });
        }

        public ActionResult Delete(string ids)
        {
            var idArray = ids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var model = new MultiDeleteModel
            {
                Count = idArray.Length,
                Id = ids,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(MultiDeleteModel model)
        {
            try
            {
                var successCount = 0;
                var idArray = model.Id.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var id in idArray)
                {
                    var result = _newsService.Delete(id);
                    if (result)
                    {
                        successCount++;
                    }
                }
                return RedirectToAction("Index", "Result", new { state = successCount > 0, message = "成功删除" + successCount + "条数据", style = "dialog" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Result", new { state = false, message = "删除失败:" + ex.Message, style = "dialog" });

            }
        }
    }
}