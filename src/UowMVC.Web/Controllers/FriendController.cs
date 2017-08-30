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
    public class FriendController : BaseController
    {
        private readonly IFriendRelationshipService _friendRelationshipService;
        public FriendController(IFriendRelationshipService friendRelationshipService)
        {
            this._friendRelationshipService = friendRelationshipService;
        }

        public ActionResult Index(string key, int pageIndex = 1)
        {
            var limit = DefaultPageSize;
            var offset = (pageIndex - 1) * limit;
            var count = 0;
            var items = _friendRelationshipService.Query(key, offset, limit, out count, UserId);
            var model = new PagedList<FriendRelationshipViewModel>(items, pageIndex, limit, count);
            ViewBag.key = key;
            return View(model);
        }

        public void Delete(string id)
        {
            _friendRelationshipService.Delete(id);
        }
    }
}