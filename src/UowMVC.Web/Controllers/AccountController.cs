using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using UowMVC.Web.Models;
using UowMVC.Domain;
using UowMVC.Service.Interfaces;
using System.Net.Http;
using UowMVC.Web.Helpers;
using Webdiyer.WebControls.Mvc;
using UowMVC.Models;
using System.Drawing;
using System.Drawing.Imaging;
using UowMVC.SDK;

namespace UowMVC.Web.Controllers
{

    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ILoginLogService _loginLogService;
        private readonly IMediaService _mediaService;

        public AccountController(IUserService userService, ILoginLogService loginLogService, IMediaService mediaService)
        {
            this._userService = userService;
            this._loginLogService = loginLogService;
            this._mediaService = mediaService;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl = "/")
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
            // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            string ip = "";
            if (Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null)
            {
                ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
            }
            else
            {
                ip = Request.ServerVariables.Get("Remote_Addr").ToString().Trim();
            }
            var place = SinaIPParser.Parse(ip);
            bool isSuccess = false;
            var message = "";
            switch (result)
            {
                case SignInStatus.Success:
                    isSuccess = true;
                    message = "登录成功";
                    break;
                case SignInStatus.Failure:
                    isSuccess = false;
                    message = "登录失败，账号或者密码不正确";
                    break;
                case SignInStatus.LockedOut:
                    isSuccess = false;
                    message = "登录失败，账户被锁定";
                    break;
                case SignInStatus.RequiresVerification:
                    isSuccess = false;
                    message = "登录失败，账户需要验证";
                    break;
                default:
                    isSuccess = false;
                    message = "登录失败，未知因素";
                    break;
            }
            var browser = Request.Browser.Browser.ToString();
            var browser_version = Request.Browser.MajorVersion.ToString();
            var browser_platform = Request.Browser.Platform.ToString();
            var client = string.Format("{0}-{1}-{2}", browser, browser_version, browser_platform);

            _loginLogService.Add(new LoginLogViewModel
            {
                IP = ip,
                Place = place,
                Result = isSuccess,
                Client = client,
                UserName = model.UserName,
                Message = message,
            });
            if (isSuccess)
            {
                var user = _userService.GetByUserName(model.UserName);
                var namecookie = new HttpCookie("Name");
                namecookie.Value = user.Name;
                Response.Cookies.Add(namecookie);
                var avatarcookie = new HttpCookie("Avatar");
                avatarcookie.Value = user.Avatar;
                Response.Cookies.Add(avatarcookie);
                return RedirectToLocal(returnUrl);
            }
            ModelState.AddModelError("", "登录失败");
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
           
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }
        [Authorize]
        public ActionResult MyProfile()
        {
            var model = new AccountProfileModel
            {
                User = _userService.GetByUserName(User.Identity.Name),
            };
            return View(model);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.ReturnUrl = Request.UrlReferrer == null ? "/" : Request.UrlReferrer.PathAndQuery;
            return View(new ChangePasswordModel());
        }
        [Authorize]
        public ActionResult LoginLog(string key, int result = -1, int pageIndex = 1)
        {

            var limit = DefaultPageSize;
            var offset = (pageIndex - 1) * limit;
            var count = 0;
            var items = _loginLogService.QueryByUser(key, UserName, offset, limit, out count, result);
            var model = new PagedList<LoginLogViewModel>(items, pageIndex, limit, count);
            ViewBag.key = key;
            ViewBag.result = result;
            return View(model);

        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var userId = AuthenticationManager.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var result = UserManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword).Result;
                if (result.Succeeded)
                {

                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    return Redirect(returnUrl);
                }

                ModelState.AddModelError("", result.Errors.FirstOrDefault());
            }

            return View(model);
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult Edit()
        {
            var mdoel = _userService.GetById(UserId);
            return View(mdoel);
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _userService.Update(model);
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }
        private const int AvastarSize = 350;
        [Authorize]
        public ActionResult ChangeAvatar(string id)
        {
            var img = _mediaService.GetById(id);
            var filename = img.RelavtivePath.Replace(@"/", @"\\");
            string filepath = WebConfig.ResourceFolder + filename;
            var resImage = Image.FromFile(filepath);

            var model = new ChangeAvatarModel
            {
                ImageId = id,
                ImageWidth = resImage.Width > AvastarSize ? AvastarSize : resImage.Width,
                ImageHeight = resImage.Height > AvastarSize ? AvastarSize : resImage.Height,
                Image = img.AbsolutePath,
            };
            resImage.Dispose();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangeAvatar(ChangeAvatarModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var width = model.Width;
            var height = model.Height;
            var x = model.X1;
            var y = model.Y1;
            var img = _mediaService.GetById(model.ImageId);
            var filename = img.RelavtivePath.Replace(@"/", @"\\");
            string filepath = WebConfig.ResourceFolder + filename;
            var resImage = Image.FromFile(filepath);
            Image resizeImage = ImageOperation.ResizeImage(resImage, model.ImageWidth, model.ImageHeight);
            if (width <= 0 || height <= 0)
            {
                width = model.ImageWidth;
                height = model.ImageHeight;
            }
            var bitmap = ImageOperation.Cut(resizeImage, x, y, width, height);
            resImage.Dispose();
            bitmap.Save(filepath);
            resizeImage.Dispose();
            bitmap.Dispose();
            _userService.ChangeAvatar(UserId, img);
            var avatarcookie = new HttpCookie("Avatar");
            avatarcookie.Value = img.AbsolutePath;
            Response.Cookies.Add(avatarcookie);
            return RedirectToAction("MyProfile");
        }
    }
}