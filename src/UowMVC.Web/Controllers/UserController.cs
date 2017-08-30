using UowMVC.Web.Helpers;
using UowMVC.Domain;
using UowMVC.Models;
using UowMVC.SDK;
using UowMVC.Service.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace UowMVC.Web.Controllers
{
    [RoleAuthorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;
        private readonly IUserGroupService _userGroupService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService,
            IDepartmentService departmentService,
            IUserGroupService userGroupService,
            IRoleService roleService)
        {
            this._userService = userService;
            this._departmentService = departmentService;
            this._userGroupService = userGroupService;
            this._roleService = roleService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="groupby">
        /// 分组视图：
        /// org：按照组织架构查看用户
        /// group：按照用户分组查看用户
        /// role：按照角色查看用户
        /// </param>
        /// <returns></returns>
        public ActionResult Index(string key, int pageIndex = 1, string groupby = "role", string fid = "")
        {
            var limit = DefaultPageSize;
            var offset = (pageIndex - 1) * limit;
            var count = 0;
            IEnumerable<UserViewModel> items = null;
            var groupbyname = "";
            switch (groupby)
            {
                default:
                    groupbyname = "角色视图";
                    items = _userService.QueryByRole(key, fid, offset, limit, out count);
                    break;
                case "group":
                    groupbyname = "用户组视图";
                    items = _userService.QueryByGroup(key, fid, offset, limit, out count);
                    break;

                case "org":
                    groupbyname = "组织架构视图";
                    items = _userService.QueryByDepartment(key, fid, offset, limit, out count);
                    break;
            }
            var model = new PagedList<UserViewModel>(items, pageIndex, limit, count);
            ViewBag.key = key;
            ViewBag.groupby = groupby;
            ViewBag.groupbyname = groupbyname;
            ViewBag.fid = fid;
            return View(model);
        }

        public ActionResult New()
        {
            var mdoel = new UserViewModel
            {
            };
            return View(mdoel);
        }

        [HttpPost]
        public ActionResult New(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = UserManager.Create(new ApplicationUser
            {
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                IsDelete = false,
                Name = model.Name,
                UserName = model.UserName,
                Num = model.Num,
                Gender = (GenderEnum)model.Gender,
                Type = ApplicationUserTypeEnum.Administrator,
            }, model.Password);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }
        public ActionResult Edit(string id)
        {
            var mdoel = _userService.GetById(id);
            return View(mdoel);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _userService.Update(model);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }

        public ActionResult Delete(string id)
        {
            var mdoel = _userService.GetById(id);
            return View(mdoel);
        }

        [HttpPost]
        public ActionResult Delete(UserViewModel model)
        {
            var user = UserManager.FindById(model.Id);
            var result = UserManager.DeleteAsync(user);
            return RedirectToAction("Index", "Result", new { state = result.Result == IdentityResult.Success, style = "dialog" });
        }

        public ActionResult Detail(string id)
        {
            var mdoel = _userService.GetById(id);
            return View(mdoel);
        }
        public JsonResult getTree(string checkedId, string groupby = "org")
        {
            IEnumerable<ZTree> nodes = null;
            switch (groupby)
            {
                case "role":
                    nodes = _roleService.GetAll().Select(x => new ZTree(x.Id, x.Name, ""));
                    break;
                case "group":
                    nodes = _userGroupService.GetAll().Select(x => new ZTree(x.Id, x.Name, ""));
                    break;
                default://Org
                    nodes = _departmentService.GetAll().Select(x => new ZTree(x.Id, x.Name, x.ParentID));
                    break;
            }
            var tree = ZTreeHelper.ToJson(nodes, "/User?groupby=" + groupby, checkedId);
            return Json(tree);
        }
    }
}