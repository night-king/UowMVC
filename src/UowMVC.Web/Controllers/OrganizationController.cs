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
    public class OrganizationController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IUserService _userService;

        public OrganizationController(IDepartmentService departmentService, IUserService userService)
        {
            this._departmentService = departmentService;
            this._userService = userService;

        }

        public ActionResult Index()
        {
            var model = _departmentService.GetAll();
            return View(model);
        }

        public ActionResult New(string id)
        {
            var mdoel = new DepartmentViewModel
            {
                ParentID = id,
            };
            return View(mdoel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult New(DepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _departmentService.Add(model);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }
        public ActionResult Edit(string id)
        {
            var mdoel = _departmentService.GetById(id);
            return View(mdoel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(DepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _departmentService.Update(model);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }

        public ActionResult Delete(string id)
        {
            var mdoel = _departmentService.GetById(id);
            return View(mdoel);
        }

        [HttpPost]
        public ActionResult Delete(DepartmentViewModel model)
        {
            var result = _departmentService.Delete(model.Id);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });

        }
        public ActionResult Assign(string id)
        {
            var model = _departmentService.GetById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Assign()
        {
            var id = Request.Form["id"];
            var type = Request.Form["type"];
            var userids = Request.Form["user"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var result = false;
            if (type == "1")
            {
                result = _userService.AddUsersToDepartment(id, userids);
            }
            else
            {
                result = _userService.RemoveUsersFromDepartment(id, userids);
            }

            var model = _departmentService.GetById(id);
            return View(model);
        }
        public PartialViewResult _Department_Left_Partial(string key, string departmentid, int pageIndex = 1)
        {
            var limit = 100;
            var offset = (pageIndex - 1) * limit;
            var count = 0;
            var items = _userService.QueryExDepartment(key, departmentid, offset, limit, out count);
            return PartialView(items);
        }

        public PartialViewResult _Department_Right_Partial(string departmentid, int pageIndex = 1)
        {
            var limit = 100;
            var offset = (pageIndex - 1) * limit;
            var count = 0;
            var items = _userService.QueryByDepartment("", departmentid, offset, limit, out count);
            return PartialView(items);
        }
    }
}