using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public enum MediaTypeEnum
    {
        [Description("图片")]
        Image = 1,

        [Description("文件")]
        File = 2,
    }
}
