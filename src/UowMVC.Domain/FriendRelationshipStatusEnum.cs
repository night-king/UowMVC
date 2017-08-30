using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    /// <summary>
    /// 朋友关系状态
    /// </summary>
    public enum FriendRelationshipStatusEnum
    {
        [Description("待审核")]
        New = 1,

        [Description("已通过")]
        Passed = 2,

        [Description("已拒绝")]
        Rejected = 3
    }
}
