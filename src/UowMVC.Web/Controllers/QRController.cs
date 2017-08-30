using UowMVC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UowMVC.Web.Controllers
{
    /// <summary>
    /// 二维码展示与解析
    /// </summary>
    public class QRController : Controller
    {
        private readonly IQrcodeService _qrcodeService;

        public QRController(IQrcodeService qrcodeService)
        {
            this._qrcodeService = qrcodeService;
        }
        /// <summary>
        /// 展示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index(string id)
        {
            var model = _qrcodeService.GetById(id);
            if (model == null || model.ExpireAt.HasValue && model.ExpireAt.Value < DateTime.Now)
            {
                return Redirect("Error");
            }
            if (string.IsNullOrEmpty(model.Content))
            {
                return Redirect("Error");
            }
            return View(model);
        }
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Get(string id)
        {
            var model = _qrcodeService.GetById(id);
            if (model == null || model.ExpireAt.HasValue && model.ExpireAt.Value < DateTime.Now)
            {
                return Redirect("Error");
            }
            if (string.IsNullOrEmpty(model.Content))
            {
                return Redirect("Error");
            }
            return Redirect(model.Content);
        }

        [AllowAnonymous]
        public ActionResult Error()
        {
            return View();
        }
    }
}