using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations.Schema;

namespace UowMVC.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public int Index { get; set; }

        public virtual Media UserAvatar { get; set; }


        [StringLength(128, MinimumLength = 0)]
        public string Num { get; set; }

        public string QQ { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public ApplicationUserTypeEnum Type { get; set; }

        [StringLength(128, MinimumLength = 0)]
        public string Name { get; set; }

        [StringLength(128, MinimumLength = 0)]
        public string RealName { get; set; }

        public DateTime CreateAt { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? DeleteAt { get; set; }

        public GenderEnum Gender { get; set; }

        public string Deparment { get; set; }

        public string Introduce { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public string BirthAt { get; set; }

        public virtual ICollection<UserGroupRelationship> UserGroups { get; set; }

        public virtual ICollection<DepartmentRelationship> Departments { get; set; }
        public bool IsSuperAdmin { get; set; }
        public ApplicationUser()
        {
            IsDelete = false;
            IsSuperAdmin = false;
            Id = Guid.NewGuid().ToString();
            CreateAt = DateTime.Now;
            Gender = GenderEnum.Unknow;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, string> manager, string authenticationType)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // 在此处添加自定义用户声明
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }
}
