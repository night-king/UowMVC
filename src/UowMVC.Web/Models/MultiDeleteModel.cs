using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UowMVC.Web.Models
{
    public class MultiDeleteModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { set; get; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int Count { set; get; }
    }
}