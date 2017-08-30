using UowMVC.Domain;
using UowMVC.Repository;
using UowMVC.SDK;
using UowMVC.Service.Interfaces;
using Autofac;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Web;

namespace UowMVC.Web
{
    /// <summary>
    /// UploadFileHandler1 的摘要说明
    /// </summary>
    public class UploadFileHandler : IHttpHandler
    {
        /// <summary>
        /// 您将需要在网站的 Web.config 文件中配置此处理程序 
        /// 并向 IIS 注册它，然后才能使用它。有关详细信息，
        /// 请参见下面的链接: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // 如果无法为其他请求重用托管处理程序，则返回 false。
            // 如果按请求保留某些状态信息，则通常这将为 false。
            get { return true; }
        }

        private int MAXSIZE = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxUploadSize"]);

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var from = context.Request.QueryString.Get("from");
                var callback = context.Request.QueryString.Get("CKEditorFuncNum");
                var imageDomain = WebConfig.ResourceDomain;
                var responseText = new StringBuilder();
                context.Response.ContentType = "text/json";
                var httpPostedFile = context.Request.Files[0];
                string fileNamePath = "";
                fileNamePath = httpPostedFile.FileName;
                var virtualPath = "/upload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                FileInfo file = new FileInfo(fileNamePath);
                string fileNameExt = file.Extension;
                var isValidExt = false;
                var baseDirectory = "";
                if (CheckExt(fileNameExt))
                {
                    isValidExt = true;
                    baseDirectory = WebConfig.ResourceFolder;
                }

                if (!isValidExt)
                {
                    if (from == "ckfinder")//富文本编辑器
                    {
                        responseText.Append("<script type=\"text/javascript\">")
                     .Append("window.parent.CKEDITOR.tools.callFunction(" + callback
                     + ",''," + "'Incorrect file format, must be .jpg/.jpeg/.gif/.bmp/.png/.mp4');")
                     .Append("</script>");
                    }

                    else
                    {
                        responseText.Append("'Incorrect file format , must be .jpg/.jpeg/.gif/.bmp/.png/.mp4'");
                    }
                    Response(context, 409, responseText.ToString());
                    return;
                }
                string serverPath = baseDirectory + virtualPath.Replace(@"/", @"\");
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }
                var fileNameWithoutExtension = RandomIdGenerator.GetRandomName();
                var fileName = fileNameWithoutExtension + fileNameExt;
                var size = httpPostedFile.ContentLength;

                if (size > MAXSIZE * 1024 * 1024)
                {
                    if (from == "ckfinder")
                    {
                        responseText.Append("<script type=\"text/javascript\">")
                        .Append("window.parent.CKEDITOR.tools.callFunction(" + callback
                    + ",''," + "'File size should not be greater than " + MAXSIZE + "MB');")
                        .Append("</script>");
                    }
                    else
                    {
                        responseText.Append("'File size should not be greater than " + MAXSIZE + "MB'");
                    }
                    Response(context, 409, "只允许上传小于 " + MAXSIZE + "MB的文件.");
                    return;
                }
                string toFileFullPath = serverPath + fileName;
                httpPostedFile.SaveAs(toFileFullPath);
                string relativePath = virtualPath + fileName;
                using (ILifetimeScope scope = AutofacConfig.Container.BeginLifetimeScope())
                {
                    var mediaService = scope.Resolve<IMediaService>();
                    var img = mediaService.Add(new UowMVC.Models.MediaViewModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = fileName,
                        Size = size,
                        Extension = fileNameExt,
                        Type = (int)MediaTypeEnum.Image,
                        RelavtivePath = relativePath,
                        ResourceDomain = WebConfig.ResourceDomain
                    });
                    if (from == "ckfinder")
                    {
                        responseText.Append("<script type=\"text/javascript\">")
                                    .Append("window.parent.CKEDITOR.tools.callFunction(" + callback
                                    + ",'" + imageDomain + relativePath + "','')")
                                    .Append("</script>");
                    }
                    else
                    {
                        responseText.Append(JsonConvert.SerializeObject(new { id = img.Id, path = img.AbsolutePath, errmsg = "成功" }));
                    }
                    Response(context, 200, responseText.ToString());
                }
            }
            catch (Exception ex)
            {
                Response(context, 500, ex.Message);
            }
        }
        private void Response(HttpContext context, int code, string message)
        {
            context.Response.ContentType = "text/html";
            context.Response.StatusCode = code;
            context.Response.Write(message);
        }

        #endregion
        #region Private Methods
        private bool CheckExt(string _ImageExt)
        {
            if (string.IsNullOrEmpty(_ImageExt)) return false;
            string[] allowExt = new string[] { ".gif", ".jpg", ".jpeg", ".bmp", ".png", ".txt", ".pdf", ".doc", ".xls", ".xlsx", ".ppt" };
            for (int i = 0; i < allowExt.Length; i++)
            {
                if (allowExt[i] == _ImageExt) { return true; }
            }
            return false;

        }

        #endregion
    }
}