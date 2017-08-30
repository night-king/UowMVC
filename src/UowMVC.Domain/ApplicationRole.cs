using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRoleTypeEnum Type { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public ApplicationRole()
        {
            this.CreateAt = DateTime.Now;
        }

        public ApplicationRole(string name) : this() { this.Name = name; }
        public ApplicationRole(string name, string description) : this(name) { this.Description = description; }

    }
}
