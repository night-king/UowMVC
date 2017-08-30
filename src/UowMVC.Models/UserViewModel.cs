using UowMVC.Domain;
using UowMVC.SDK;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "序号")]
        public int Index { get; set; }


        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "密码")]
        [StringLength(12, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "手机号")]
        public string PhoneNumber { get; set; }

        public string Avatar { get; set; }
        [Display(Name = "头像")]
        public string AvatarId { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "性别")]
        public int Gender { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "QQ")]
        public string QQ { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "编号")]
        public string Num { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "真实姓名")]
        public string RealName { get; set; }

        public ApplicationUserTypeEnum Type { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "称呼")]
        public string Name { get; set; }

        [DataType(DataType.Text)]

        [Display(Name = "所在学院")]
        public string Deparment { get; set; }

        [Display(Name = "角色")]
        public IEnumerable<string> RoleIds { get; set; }

        [Display(Name = "简介")]
        public string Introduce { get; set; }
        public DateTime CreateAt { get; set; }
        public IEnumerable<UserGroupViewModel> UserGroups { get; set; }

        public IEnumerable<DepartmentViewModel> Departments { get; set; }

        public UserViewModel()
        {
        }

        public UserViewModel(ApplicationUser user)
        {
            Id = user.Id;
            Name = user.Name;
            Deparment = user.Deparment;
            UserName = user.UserName;
            Email = user.Email;
            Avatar = user.UserAvatar == null ? "/images/default-avatar.png" : user.UserAvatar.AbsolutePath;
            AvatarId = user.UserAvatar == null ? "" : user.UserAvatar.Id;
            Num = user.Num;
            Type = user.Type;
            CreateAt = user.CreateAt;
            RoleIds = user.Roles != null && user.Roles.Count > 0 ? user.Roles.Select(x => x.RoleId) : null;
            PhoneNumber = user.PhoneNumber;
            Gender = (int)user.Gender;
            RealName = user.RealName;
            Introduce = user.Introduce;
            UserGroups = user.UserGroups == null ? new List<UserGroupViewModel>() : user.UserGroups.Select(x => new UserGroupViewModel(x.UserGroup));
            Departments = user.Departments == null ? new List<DepartmentViewModel>() : user.Departments.Select(x => new DepartmentViewModel(x.Department));
        }
    }
}
