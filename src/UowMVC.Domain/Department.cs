using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    /// <summary>
    /// 学院、班级
    /// </summary>
    public class Department : BaseEntity
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

        public string Description
        {
            set; get;
        }

        public virtual Department Parent
        {
            set; get;
        }
        /// <summary>
        /// 管理员
        /// </summary>
        public virtual ICollection<DepartmentRelationship> Relationships
        {
            set; get;
        }
    }
}
