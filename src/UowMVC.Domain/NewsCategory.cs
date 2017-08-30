using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    /// <summary>
    /// 资讯分类
    /// </summary>
    public class NewsCategory : FakeDeleteEntity
    {
        public int No
        {
            set; get;
        }
        [StringLength(256, MinimumLength = 0)]
        public string Name
        {
            set; get;
        }

    }
}
