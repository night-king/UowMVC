using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UowMVC.Web.Models
{
    public class ResultModel
    {
        public bool State { set; get; }
        public string Title { set; get; }

        public string Message { set; get; }
        public string ReturnUrl { set; get; }

    }
}