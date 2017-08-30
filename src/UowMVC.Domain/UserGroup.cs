using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public class UserGroup : BaseEntity
    {
        [StringLength(128, MinimumLength = 0)]
        public string No
        {
            set; get;
        }
        [StringLength(128, MinimumLength = 0)]
        public string Name
        {
            set; get;
        }
        [StringLength(1000, MinimumLength = 0)]
        public string Description
        {
            set; get;
        }
    }
}
