using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    /// <summary>
    /// 资讯浏览记录
    /// </summary>
    public class NewsViewRecord : BaseEntity
    {
        public virtual string NewsId
        {
            set; get;
        }
        public virtual string NewsTitle
        {
            set; get;
        }
        public virtual string StudentNO
        {
            set; get;
        }
        public virtual string StudentId
        {
            set; get;
        }
    }
}
