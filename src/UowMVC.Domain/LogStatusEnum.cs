using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public enum LogStatusEnum
    {
        [Description("新生成")]
        New = 1,

        [Description("已上报")]
        Reported = 2,

        [Description("已解决")]
        Resolved = 4,
    }
}
