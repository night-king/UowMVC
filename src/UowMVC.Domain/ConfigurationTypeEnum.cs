using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public enum ConfigurationTypeEnum
    {
        [Description("数字")]
        Int = 1,

        [Description("是非")]
        Bool = 2,

        [Description("字符串")]
        String = 3,

        [Description("图片")]
        Image = 4
    }
}
