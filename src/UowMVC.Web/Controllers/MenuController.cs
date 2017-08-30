using UowMVC.Web.Helpers;
using UowMVC.Web.Models;
using UowMVC.Domain;
using UowMVC.Models;
using UowMVC.SDK;
using UowMVC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace UowMVC.Web.Controllers
{
    [RoleAuthorize]
    public class MenuController : BaseController
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            this._menuService = menuService;
        }

        // GET: Menu
        public ActionResult Index()
        {
            var model = _menuService.GetAll();
            return View(model);
        }
        public ActionResult New(string id)
        {
            var mdoel = new MenuViewModel
            {
                ParentID = id,
            };
            return View(mdoel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult New(MenuViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            MenuConfig.Clear();
            if (!_menuService.Verify(model))
            {
                ModelState.AddModelError("", "URL冲突，请检查");
                return View(model);
            }
            var result = _menuService.Add(model);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }
        public ActionResult Edit(string id)
        {
            var mdoel = _menuService.GetById(id);
            return View(mdoel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(MenuViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!_menuService.Verify(model))
            {
                ModelState.AddModelError("", "URL冲突，请检查");
                return View(model);
            }
            var result = _menuService.Update(model);
            MenuConfig.Clear();
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }

        public ActionResult Delete(string id)
        {
            var mdoel = _menuService.GetById(id);
            return View(mdoel);
        }

        [HttpPost]
        public ActionResult Delete(MenuViewModel model)
        {
            var result = _menuService.Delete(model.Id);
            MenuConfig.Clear();
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }

        public ActionResult Import()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Import(ImportModel model)
        {
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
            return RedirectToAction("Index", "Result", new { state = true, style = "dialog" });
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
        public ActionResult Export()
        {
            var virtualPath = string.Format("{0}/{1}/{2}/{3}{4}", "/Files", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), RandomIdGenerator.NewId(), ".xml");
            var absolutePath = Server.MapPath("~/") + virtualPath.Replace("/", "\\");
            var dir = Path.GetDirectoryName(absolutePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            using (StreamWriter sr = new StreamWriter(absolutePath))
            {
                var serializer = new XmlSerializer(typeof(List<MenuViewModel>));
                var menus = uow.Set<Menu>().ToList().Select(x => new MenuViewModel(x)).ToList();
                serializer.Serialize(sr, menus);
            }
            return File(absolutePath, "application/xml");
        }

        public ActionResult ExportControlPanel()
        {
            var virtualPath = string.Format("{0}/{1}/{2}/{3}{4}", "/Files", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), RandomIdGenerator.NewId(), ".xml");
            var absolutePath = Server.MapPath("~/") + virtualPath.Replace("/", "\\");
            var dir = Path.GetDirectoryName(absolutePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            using (StreamWriter sr = new StreamWriter(absolutePath))
            {
                var serializer = new XmlSerializer(typeof(List<MenuViewModel>));
                var menus = uow.Set<Menu>().Where(x => x.IsControlPanel == true).ToList().Select(x => new MenuViewModel(x));
                var result = new List<MenuViewModel>();
                var roots = menus.Where(x => string.IsNullOrEmpty(x.ParentID)).OrderBy(x => x.No).ToList();
                foreach (var root in roots)
                {
                    result.Add(root);
                    appendChildren(result, menus, root);
                }
                serializer.Serialize(sr, result);
            }
            return File(absolutePath, "application/xml");
        }

        private void appendChildren(List<MenuViewModel> container, IEnumerable<MenuViewModel> source, MenuViewModel node)
        {
            var children = source.Where(x => x.ParentID == node.Id).OrderBy(x => x.No).ToList();
            foreach (var child in children)
            {
                container.Add(child);
                appendChildren(container, source, child);
            }
        }

    }
}