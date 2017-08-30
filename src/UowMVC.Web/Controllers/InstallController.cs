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
using Microsoft.AspNet.Identity;

namespace UowMVC.Web.Controllers
{
    [Authorize]
    public class InstallController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ImportModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = UserManager.FindById(UserId);
            if (!user.IsSuperAdmin)
            {
                return RedirectToAction("Index", "Result", new { state = false, message = "安装失败，您没有此权限", returnUrl = "/" });
            }
            var file = Request.Files[0];
            var name = file.FileName;
            var ext = Path.GetExtension(name);
            var virtualPath = string.Format("{0}/{1}/{2}/{3}{4}", "/Files", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), RandomIdGenerator.NewId(), ext);
            var absolutePath = Server.MapPath("~/") + virtualPath.Replace("/", "\\");
            var dir = Path.GetDirectoryName(absolutePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            file.SaveAs(absolutePath);
            var permissions = uow.Set<RolePermission>().ToList();
            for (int i = 0; i < permissions.Count; i++)
            {
                uow.Set<RolePermission>().Remove(permissions[i]);
            }
            var menus = uow.Set<Menu>().ToList();
            for (int i = 0; i < menus.Count; i++)
            {
                uow.Set<Menu>().Remove(menus[i]);
            }
            uow.Commit();
            using (StreamReader sr = new StreamReader(absolutePath))
            {
                var serializer = new XmlSerializer(typeof(List<MenuViewModel>));
                var newMenus = (List<MenuViewModel>)serializer.Deserialize(sr);
                if (newMenus != null)
                {
                    var fathers = newMenus.Where(x => string.IsNullOrEmpty(x.ParentID)).ToList();
                    foreach (var fa in fathers)
                    {
                        uow.Set<Menu>().Add(new Menu
                        {
                            Id = fa.Id,
                            IsControlPanel = fa.IsControlPanel,
                            Description = fa.Description,
                            CreateAt = DateTime.Now,
                            Height = fa.Height,
                            Icon = fa.Icon,
                            IsDisplayOnTable = fa.IsDisplayOnTable,
                            IsMustSelected = fa.IsMustSelected,
                            Name = fa.Name,
                            No = fa.No,
                            OpenStyle = (MenuOpenStyleEnum)fa.OpenStyle,
                            RelevantURL = fa.RelevantURL,
                            URL = fa.URL,
                            Width = fa.Width,
                        });
                        findChildren(newMenus, fa);
                    }
                }
            }
            uow.Commit();
            MenuConfig.Clear();
            return RedirectToAction("Index", "Result", new { state = true, message = "安装成功，请重新配置角色权限", returnUrl = "/" });
        }

        private void findChildren(List<MenuViewModel> root, MenuViewModel current)
        {
            var fathers = root.Where(x => x.ParentID == current.Id).ToList();
            foreach (var fa in fathers)
            {
                uow.Set<Menu>().Add(new Menu
                {
                    Id = fa.Id,
                    IsControlPanel = fa.IsControlPanel,
                    Description = fa.Description,
                    CreateAt = DateTime.Now,
                    Height = fa.Height,
                    Icon = fa.Icon,
                    IsDisplayOnTable = fa.IsDisplayOnTable,
                    IsMustSelected = fa.IsMustSelected,
                    Name = fa.Name,
                    No = fa.No,
                    OpenStyle = (MenuOpenStyleEnum)fa.OpenStyle,
                    RelevantURL = fa.RelevantURL,
                    URL = fa.URL,
                    Width = fa.Width,
                    Parent = uow.Set<Menu>().Find(fa.ParentID),
                });
                findChildren(root, fa);
            }
        }
    }
}