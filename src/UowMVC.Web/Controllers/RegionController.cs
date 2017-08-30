using UowMVC.Web.Helpers;
using UowMVC.Web.Models;
using UowMVC.Domain;
using UowMVC.SDK;
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
    public class RegionController : BaseController
    {
        [RoleAuthorize]
        public ActionResult Index(string key, int pageIndex = 1)
        {
            int count = 0;
            var offset = (pageIndex - 1) * DefaultPageSize;
            var query = uow.Set<Region>().AsQueryable();
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Code.Contains(key) || x.Province.Contains(key) || x.City.Contains(key) || x.Area.Contains(key));
            }
            ViewBag.pageIndex = pageIndex; ViewBag.key = key;
            var items = query.OrderByDescending(x => x.CreateAt).Skip(offset).Take(DefaultPageSize).ToList();
            count = query.Count();
            ViewBag.pageIndex = pageIndex; ViewBag.key = key;
            var model = new PagedList<Region>(items, pageIndex, DefaultPageSize, count);
            return View(model);
        }
        [RoleAuthorize]
        public ActionResult Import()
        {
            return View();
        }

        [RoleAuthorize]
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
            var dt = NPOIHelper.Import(absolutePath);
            if (dt == null || dt.Rows.Count == 0)
            {
                return RedirectToAction("Index", "Result", new { state = false, message = "导入失败，没有读取到内容", style = "dialog" });
            }
            var count = 0;
            foreach (DataRow row in dt.Rows)
            {
                var code = row[0].ToString();
                var province = row[1].ToString();
                var city = row[2].ToString();
                var area = row[3].ToString();
                var region = new Region
                {
                    Area = area,
                    City = city,
                    Code = code,
                    Province = province,
                };
                uow.Set<Region>().Add(region);
                uow.Commit();
                count++;
            }
            return RedirectToAction("Index", "Result", new { state = true, message = "导入成功，共导入" + count + "条数据", style = "dialog" });

        }

        public JsonResult Query(string key, int level, string fname, int offset, int limit)
        {
            switch (level)
            {
                case 1:
                    {
                        var query = uow.Set<Region>().AsQueryable();
                        if (!string.IsNullOrEmpty(key))
                        {
                            query = query.Where(x => x.Province.Contains(key));
                        }
                        return Json(query.OrderBy(x => x.Code).Select(x => x.Province).Distinct().ToList()
                            .Select(x => new
                            {
                                name = x,
                                level = 1,
                            }), JsonRequestBehavior.AllowGet);
                    }
                case 2:
                    {
                        var query = uow.Set<Region>().Where(x => x.Province == fname).AsQueryable();
                        if (!string.IsNullOrEmpty(key))
                        {
                            query = query.Where(x => x.City.Contains(key));
                        }
                        return Json(query.OrderBy(x => x.Code).Select(x => x.City).Distinct().ToList()
                            .Select(x => new
                            {
                                name = x,
                                level = 2,
                            }), JsonRequestBehavior.AllowGet);
                    }
                case 3:
                    {
                        var query = uow.Set<Region>().Where(x => x.City == fname).AsQueryable();
                        if (!string.IsNullOrEmpty(key))
                        {
                            query = query.Where(x => x.Area.Contains(key));
                        }
                        return Json(query.OrderBy(x => x.Code).Select(x => x.Area).Distinct().ToList()
                            .Select(x => new
                            {
                                name = x,
                                level = 3
                            }), JsonRequestBehavior.AllowGet);
                    }

                default:
                    return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}