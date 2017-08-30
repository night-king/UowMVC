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
    public class UserGroupController : Controller
    {
        private readonly IUserGroupService _userGroupService;
        private readonly IUserService _userService;
        public UserGroupController(IUserGroupService userGroupService, IUserService userService)
        {
            this._userGroupService = userGroupService;
            this._userService = userService;
        }

        public ActionResult Index()
        {
            var model = _userGroupService.GetAll();
            return View(model);
        }



        public ActionResult New()
        {
            var mdoel = new UserGroupViewModel
            {
            };
            return View(mdoel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult New(UserGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _userGroupService.Add(model);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }
        public ActionResult Edit(string id)
        {
            var mdoel = _userGroupService.GetById(id);
            return View(mdoel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(UserGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _userGroupService.Update(model);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }

        public ActionResult Delete(string id)
        {
            var mdoel = _userGroupService.GetById(id);
            return View(mdoel);
        }

        [HttpPost]
        public ActionResult Delete(UserGroupViewModel model)
        {
            var result = _userGroupService.Delete(model.Id);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });

        }
        public ActionResult Assign(string id)
        {
            var model = _userGroupService.GetById(id);
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
                result = _userService.AddUsersToGroup(id, userids);
            }
            else
            {
                result = _userService.RemoveUsersFromGroup(id, userids);
            }

            var model = _userGroupService.GetById(id);
            return View(model);
        }
        public PartialViewResult _Group_Left_Partial(string key, string groupid, int pageIndex = 1)
        {
            var limit = 100;
            var offset = (pageIndex - 1) * limit;
            var count = 0;
            var items = _userService.QueryExGroup(key, groupid, offset, limit, out count);
            return PartialView(items);
        }

        public PartialViewResult _Group_Right_Partial(string groupid, int pageIndex = 1)
        {
            var limit = 100;
            var offset = (pageIndex - 1) * limit;
            var count = 0;
            var items = _userService.QueryByGroup("", groupid, offset, limit, out count);
            return PartialView(items);
        }
    }
}