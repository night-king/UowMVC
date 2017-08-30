using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public class Dict : BaseEntity
    {
        [StringLength(128, MinimumLength = 0)]
        public string No
        {
            set; get;
        }
        [StringLength(256, MinimumLength = 0)]
        public string Key
        {
            set; get;
        }
        public string Value
        {
            set; get;
        }
        public string Description
        {
            set; get;
        }
        public virtual DictIndex Index
        {
            set; get;
        }
    }
}
