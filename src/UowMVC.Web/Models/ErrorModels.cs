using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UowMVC.Web.Models
{
    public class ErrorModel
    {
        public ErrorBrowserModel Browser { set; get; }
        public ErrorExceptionModel Exception { set; get; }
        public string Url { set; get; }

        public string IP { set; get; }
        public int StatusCode { set; get; }
        public string Id { set; get; }

    }
    public class ErrorBrowserModel
    {
        public string Browser { set; get; }
        public string MajorVersion { set; get; }
        public string Platform { set; get; }
    }
    public class ErrorExceptionModel
    {
        public string Message { set; get; }
        public string Source { set; get; }

        public string TargetSite { set; get; }
        public string StackTrace { set; get; }
    }
}