using UowMVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UowMVC.Web.Controllers
{
    [AllowAnonymous]
    public class ResultController : Controller
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state">成功or失败</param>
        /// <param name="title">提示标题</param>
        /// <param name="message">提示描述信息</param>
        /// <param name="returnUrl">
        /// 如果是Frame,请务必传递此参数值；
        /// 打开的是page,则不是必填，默认刷新父页面
        /// </param>
        /// <returns></returns>
        public ActionResult Index(bool state, string title = "", string message = "", string returnUrl = "")
        {
            if (string.IsNullOrEmpty(title))
            {
                title = state ? "成功" : "失败";
            }
            if (string.IsNullOrEmpty(message))
            {
                message = state ? "您的操作已经生效!" : "操作失败，请重试!";
            }
            var model = new ResultModel
            {
                Title = title,
                State = state,
                ReturnUrl = returnUrl,
                Message = message
            };
            return View(model);
        }
    }
}