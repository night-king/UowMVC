using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    /// <summary>
    /// 资讯
    /// </summary>
    public class News : BaseEntity
    {
        public int Index { set; get; }

        public int Year { set; get; }

        [StringLength(256, MinimumLength = 0)]
        public string Title
        {
            set; get;
        }
        public int ViewCount
        {
            set; get;
        }
        public virtual NewsCategory Category
        {
            set; get;
        }

        public string Content
        {
            set; get;
        }
    }
}
