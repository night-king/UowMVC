using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    /// <summary>
    /// 系统日志
    /// </summary>
    public class Log
    {
        public string Id { set; get; }
        [StringLength(128, MinimumLength = 0)]
        public string Thread { set; get; }
        [StringLength(128, MinimumLength = 0)]
        public string Level { set; get; }
        [StringLength(128, MinimumLength = 0)]
        public string Logger { set; get; }
        [StringLength(128, MinimumLength = 0)]
        public string UserIP { set; get; }
        [StringLength(1000, MinimumLength = 0)]
        public string URL { set; get; }
        [StringLength(128, MinimumLength = 0)]
        public string UserName { set; get; }

        [StringLength(128, MinimumLength = 0)]
        public string Action { get; set; }

        [StringLength(1000, MinimumLength = 0)]
        public string Client { set; get; }
        [StringLength(128, MinimumLength = 0)]
        public string StatusCode { set; get; }

        public string Message { set; get; }
        public string Exception { set; get; }

        public LogStatusEnum Status { set; get; }

        public DateTime CreateAt { set; get; }


    }
}
