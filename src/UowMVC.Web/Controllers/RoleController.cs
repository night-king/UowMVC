using UowMVC.Web.Models;
using UowMVC.Models;
using UowMVC.Service.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UowMVC.Web.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly IMenuService _menuService;
        private readonly IUserService _userService;

        public RoleController(IRoleService roleService, IMenuService menuService, IUserService userService)
        {
            this._roleService = roleService;
            this._menuService = menuService;
            this._userService = userService;
        }

        public ActionResult Index(string key, int pageIndex = 1)
        {
            var limit = DefaultPageSize;
            var offset = (pageIndex - 1) * limit;
            var count = 0;
            var model = _roleService.Query(key, offset, limit, out count);

            return View(model);
        }
        public ActionResult New()
        {
            var mdoel = new RoleViewModel();
            return View(mdoel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult New(RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _roleService.Add(model);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }
        public ActionResult Edit(string id)
        {
            var mdoel = _roleService.GetById(id);
            return View(mdoel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _roleService.Update(model);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }

        public ActionResult Delete(string id)
        {
            var mdoel = _roleService.GetById(id);
            return View(mdoel);
        }

        [HttpPost]
        public ActionResult Delete(RoleViewModel model)
        {
            var result = _roleService.Delete(model.Id);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }

        public ActionResult Permission(string id)
        {
            var role = _roleService.GetById(id);
            var permissions = _roleService.GetPermission(id);
            var menus = _menuService.GetAll();
            var model = new List<RolePermissionViewModel>();
            foreach (var menu in menus)
            {
                var isChecked = permissions.Any(x => x.MenuID == menu.Id && x.IsChecked == true);
                model.Add(new RolePermissionViewModel(menu, isChecked));
            }
            ViewBag.roleName = role.Name;
            ViewBag.id = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Permission()
        {
            var id = Request.Form["Id"];
            var permissions = new List<RolePermissionViewModel>();
            if (Request.Form["Menu"] != null)
            {
                var menuArray = Request.Form["Menu"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var menuId in menuArray)
                {
                    var menu = _menuService.GetById(menuId);
                    if (menu == null) { continue; }
                    permissions.Add(new RolePermissionViewModel(menu, true));
                }
            }
            _roleService.SavePermission(id, permissions);
            MenuConfig.Clear();
            return RedirectToAction("Index", "Result", new { state = true, returnUrl = "/Role" });
        }

        public ActionResult Assign(string id)
        {
            var model = _roleService.GetById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Assign()
        {
            var id = Request.Form["id"];
            var type = Request.Form["type"];
            var userids = Request.Form["user"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var role = _roleService.GetById(id);
            if (type == "1")
            {
                foreach (var userid in userids)
                {
                    UserManager.AddToRole(userid, role.Name);
                }
            }
            else
            {
                foreach (var userid in userids)
                {
                    UserManager.RemoveFromRole(userid, role.Name);
                }
            }

            var model = _roleService.GetById(id);
            return View(model);
        }

        public PartialViewResult _Role_Left_Partial(string key, string roleid, int pageIndex = 1)
        {
            var limit = 100;
            var offset = (pageIndex - 1) * limit;
            var count = 0;
            var items = _userService.QueryExRole(key, roleid, offset, limit, out count);
            return PartialView(items);
        }

        public PartialViewResult _Role_Right_Partial(string roleid, int pageIndex = 1)
        {
            var limit = 100;
            var offset = (pageIndex - 1) * limit;
            var count = 0;
            var items = _userService.QueryByRole("", roleid, offset, limit, out count);
            return PartialView(items);
        }
    }
}