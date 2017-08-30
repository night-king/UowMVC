using UowMVC.Web.Helpers;
using UowMVC.Web.Models;
using UowMVC.Domain;
using UowMVC.Models;
using UowMVC.SDK;
using UowMVC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace UowMVC.Web.Controllers
{
    [RoleAuthorize]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;
        private readonly IUserService _userService;
        readonly IMediaService mediaService;
        public DepartmentController(IDepartmentService departmentService,
            IUserService userService, IMediaService mediaService)
        {
            this._departmentService = departmentService;
            this._userService = userService;
            this.mediaService = mediaService;
        }

        public ActionResult Index(string key, int pageIndex = 1)
        {
            var limit = DefaultPageSize;
            var offset = (pageIndex - 1) * limit;
            var count = 0;
            var items = _departmentService.Query(key, offset, limit, out count);
            var model = new PagedList<DepartmentViewModel>(items, pageIndex, limit, count);
            ViewBag.key = key;
            return View(model);
        }
        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(ImportModel model)
        {
            var file = Request.Files[0];
            var fileName = file.FileName;
            var ext = Path.GetExtension(fileName);
            var virtualPath = string.Format("{0}/{1}/{2}/{3}{4}", "/Files", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), RandomIdGenerator.NewId(), ext);
            var absolutePath = Server.MapPath("~/") + virtualPath.Replace("/", "\\");
            var dir = Path.GetDirectoryName(absolutePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            file.SaveAs(absolutePath);
            mediaService.Add(new UowMVC.Models.MediaViewModel
            {
                Name = model.File,
                Size = file.ContentLength,
                Type = (int)MediaTypeEnum.File,
                Extension = ext,
                RelavtivePath = virtualPath,
                ResourceDomain = WebConfig.ResourceDomain
            });
            var dt = NPOIHelper.Import(absolutePath);
            if (dt == null || dt.Rows.Count == 0)
            {
                return RedirectToAction("Index", "Result", new { state = false, message = "导入失败，没有读取到内容", style = "dialog" });
            }
            var count = 0;
            foreach (DataRow row in dt.Rows)
            {
                var no = 0;
                int.TryParse(row[0].ToString(), out no);
                var name = row[1].ToString();
                var description = row[2].ToString();

                var department = new Department
                {
                    No = no,
                    Name = name,
                    Description = description,
                };
                uow.Set<Department>().Add(department);
                uow.Commit();
                count++;
            }
            return RedirectToAction("Index", "Result", new { state = true, message = "导入成功，共导入" + count + "条数据", style = "dialog" });
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

    }
}