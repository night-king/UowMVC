using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public enum GenderEnum
    {
        [Description("男")]
        Man = 1,

        [Description("女")]
        Female = 2,

        [Description("未知")]
        Unknow = 0,
    }
}
