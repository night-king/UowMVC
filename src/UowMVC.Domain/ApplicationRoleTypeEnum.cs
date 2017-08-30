using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public enum ApplicationRoleTypeEnum
    {
        [Description("超级管理员")]
        Administrator = 1,

        [Description("管理员")]
        Admin = 2,

        [Description("一般用户")]
        User = 3,

    }
}
