using UowMVC.Domain;
using UowMVC.Models;
using UowMVC.SDK;
using UowMVC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UowMVC.Web.Controllers
{
    [RoleAuthorize]
    public class ConfigrutionController : BaseController
    {
        private readonly IConfigurationService _configurationService;
        public ConfigrutionController(IConfigurationService configurationService)
        {
            this._configurationService = configurationService;
        }

        public ActionResult Index()
        {
            var model = _configurationService.GetAll();
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            ViewData["Types"] = getConfigTypes();
            var mdoel = _configurationService.GetById(id);
            return View(mdoel);
        }

        [HttpPost]
        public ActionResult Edit(ConfigurationViewModel model)
        {
            ViewData["Types"] = getConfigTypes((int)model.Type);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _configurationService.Update(model);
            WebConfig.Init();//更新配置
            return RedirectToAction("Index", "Result", new { state = result, style = "dialog" });
        }
     
        public SelectList getConfigTypes(int selected = 0)
        {
            var types = new ConfigurationTypeEnum[] { ConfigurationTypeEnum.Bool, ConfigurationTypeEnum.Image, ConfigurationTypeEnum.Int, ConfigurationTypeEnum.String };
            var list = new List<SelectListItem>();
            foreach (var m in types)
            {
                list.Add(new SelectListItem
                {
                    Selected = (int)m == selected,
                    Text = m.ToDescription(),
                    Value = ((int)m).ToString(),
                });
            }
            return new SelectList(list, "Value", "Text");
        }
    }
}