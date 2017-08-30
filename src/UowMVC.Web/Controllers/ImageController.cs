using UowMVC.Repository;
using Autofac;
using Autofac.Integration.Owin;
using Simple.ImageResizer;
using Simple.ImageResizer.MvcExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UowMVC.Web.Controllers
{
    public class ImageController : Controller
    {
        public DefaultDataContext Ctx
        {
            get
            {
                return HttpContext.GetOwinContext().GetAutofacLifetimeScope().Resolve<DefaultDataContext>();
            }
        }

        // GET: Img
        [OutputCache(VaryByParam = "*", Duration = 60 * 60 * 24 * 365)]
        public ImageResult Index(string id, int w = 0, int h = 0)
        {
            var img = Ctx.Images.Find(id);
            var filename = img.RelavtivePath.Replace(@"/", @"\\");
            string filepath = WebConfig.ResourceFolder + filename;
            return new ImageResult(filepath, w, h);
        }
    }
}