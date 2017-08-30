using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public class LoginLog : BaseEntity
    {
        [StringLength(128, MinimumLength = 0)]
        public string IP { set; get; }

        [StringLength(128, MinimumLength = 0)]
        public string Place { set; get; }

        [StringLength(256, MinimumLength = 0)]
        public string Client { set; get; }

        public bool Result { set; get; }

        [StringLength(1000, MinimumLength = 0)]
        public string Message { set; get; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        public virtual string UserName { set; get; }

    }
}
