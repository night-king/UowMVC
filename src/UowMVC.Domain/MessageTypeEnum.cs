using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    [Flags]
    public enum MessageTypeEnum
    {
        [Description("系统消息")]
        System = 1,

        [Description("用户消息")]
        User = 2
    }
}
