using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public class RolePermission : BaseEntity
    {
        public virtual Menu Menu { set; get; }

        public virtual ApplicationRole Role { set; get; }

        public virtual bool IsChecked { set; get; }
    }
}
