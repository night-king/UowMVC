using UowMVC.Web.Models;
using UowMVC.Web.SignalR;
using UowMVC.Domain;
using UowMVC.Models;
using UowMVC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace UowMVC.Web.Controllers
{
    [Authorize]
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public MessageController(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type">
        /// 1： 收件箱；
        /// 2： 发件箱；
        /// 3： 草稿箱；
        /// 4： 回收站
        /// </param>
        /// <param name="tag">
        /// 0： 全部；
        /// 1： 系统；
        /// 2： 私人；
        /// </param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Index(string key, int type = 1, int tag = 0, int pageIndex = 1)
        {
            var limit = DefaultPageSize;
            var offset = (pageIndex - 1) * limit;
            var count = 0;
            IEnumerable<MessageViewModel> items = null;
            var tagEnum = Domain.MessageTypeEnum.System | Domain.MessageTypeEnum.User;
            switch (tag)
            {
                case 1:
                    tagEnum = Domain.MessageTypeEnum.System;
                    break;
                case 2:
                    tagEnum = Domain.MessageTypeEnum.User;
                    break;
            }
            switch (type)
            {
                case 1:
                    items = _messageService.QueryAccept(key, offset, limit, out count,
                            Domain.MessageStatusEnum.New | Domain.MessageStatusEnum.Readed,
                            tagEnum,
                            UserId);
                    break;
                case 2:
                    items = _messageService.QuerySend(key, offset, limit, out count,
                            Domain.MessageStatusEnum.New | Domain.MessageStatusEnum.Readed,
                            tagEnum,
                            UserId);
                    break;
                case 3:
                    items = _messageService.QuerySend(key, offset, limit, out count,
                            Domain.MessageStatusEnum.Draft,
                            tagEnum,
                            UserId);
                    break;
                case 4:
                    items = _messageService.QueryRecycle(key, offset, limit, out count, UserId);
                    break;
            }
            var model = new PagedList<MessageViewModel>(items, pageIndex, limit, count);
            ViewBag.key = key;
            return View(model);
        }

        public void Delete(string id)
        {
            foreach (var i in id.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                _messageService.Delete(id);
            }
        }

        public void Remove(string id)
        {
            foreach (var i in id.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                _messageService.Remove(id);
            }
        }
        public void Read(string id)
        {
            foreach (var i in id.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                _messageService.Read(id);
            }
        }
        public void UnRead(string id)
        {
            foreach (var i in id.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                _messageService.UnRead(id);
            }
        }
        public ActionResult Detail(string id)
        {
            var model = _messageService.GetById(id, true);
            return View(model);
        }
        public ActionResult New(string username)
        {
            var model = new MessageRegisterModel
            {
                Accepter = username,
                IsDraft = false,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult New(MessageRegisterModel model)
        {
            var accepters = new List<UserViewModel>();
            var accepterNames = model.Accepter.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var u in accepterNames)
            {
                var accepter = _userService.GetByUserName(u);
                if (accepter == null || string.IsNullOrEmpty(accepter.Id))
                {
                    ModelState.AddModelError("Accepter", "用户" + u + "不存在");
                    return View(model);
                }
                accepters.Add(accepter);
            }
            if (accepters.Count == 0 && model.IsDraft == false)
            {
                ModelState.AddModelError("Accepter", "收件人不能为空");
                return View(model);
            }
            var sender = _userService.GetById(UserId);
            foreach (var m in accepters)
            {
                var message = new MessageViewModel
                {
                    Accepter = m,
                    Content = model.Content,
                    Sender = sender,
                    Title = model.Title,
                    Status = model.IsDraft ? (int)MessageStatusEnum.Draft : (int)MessageStatusEnum.New,
                    Type = (int)MessageTypeEnum.User,
                };
                _messageService.Add(message);
                PushHelper.PushMessage(m.UserName);
            }
            return RedirectToAction("Index", new { type = 2 });
        }

        public PartialViewResult _Mail_Menu_Partial()
        {
            var model = new MailMenuModel
            {
                ReceiedNewCount = _messageService.QueryAcceptCount(UserId, Domain.MessageStatusEnum.New, Domain.MessageTypeEnum.System | Domain.MessageTypeEnum.User),
                DraftCount = _messageService.QueryAcceptCount(UserId, Domain.MessageStatusEnum.Draft, Domain.MessageTypeEnum.System | Domain.MessageTypeEnum.User),
                PrivateNewCount = _messageService.QueryAcceptCount(UserId, Domain.MessageStatusEnum.New, Domain.MessageTypeEnum.User),
                SystemNewCount = _messageService.QueryAcceptCount(UserId, Domain.MessageStatusEnum.New, Domain.MessageTypeEnum.System)
            };
            return PartialView(model);
        }

        public ActionResult QueryAccepters(string q, int size = 10)
        {
            var count = 0;
            var items = _userService.Query(q, 0, size, out count);
            return Json(items.Take(size).Select(x => new { Id = x.Id, Name = x.UserName, }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMyNewMessage()
        {
            var newCount = 0;
            var userName = User.Identity.Name;
            var items = _messageService.QueryReceiveNew(userName, out newCount);
            var tempEntity = new
            {
                count = newCount,
                items = items.Select(x => new
                {
                    id = x.Id,
                    avatar = x.Sender == null ? "" : x.Sender.Avatar,
                    title = x.Title == null ? "" : (x.Title.Length > 10 ? x.Title.Substring(0, 10) + "..." : x.Title),
                    time = x.CreateAt.ToString("yyyy-MM-dd HH:mm:ss")
                })
            };
            return Json(tempEntity, JsonRequestBehavior.AllowGet);
        }
    }
}