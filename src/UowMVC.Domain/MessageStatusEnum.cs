using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    [Flags]
    public enum MessageStatusEnum
    {
        [Description("草稿")]
        Draft = 0,

        [Description("未读")]
        New = 1,

        [Description("已读")]
        Readed = 2
    }
}
