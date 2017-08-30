using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UowMVC.Web.Models
{
    public class ChangeAvatarModel
    {
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }

        public string ImageId { get; set; }
        public string Image { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int X1 { get; set; }

        public int X2 { get; set; }
        public int Y1 { get; set; }

        public int Y2 { get; set; }

    }
    public class LoginModel
    {
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Password)]
        [Display(Name = "当前密码")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Password)]
        [Display(Name = "用户")]
        public string Id { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

    }
}
