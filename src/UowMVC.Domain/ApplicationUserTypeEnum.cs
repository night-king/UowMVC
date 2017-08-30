using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public enum ApplicationUserTypeEnum
    {
        [Description("管理员")]
        Administrator = 1,

        [Description("用户")]
        User = 2,
    }
}
