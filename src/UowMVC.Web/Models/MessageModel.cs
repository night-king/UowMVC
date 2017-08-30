using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UowMVC.Web.Models
{

    public class MessageRegisterModel
    {
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "收件人")]
        public string Accepter { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [MinLength(3, ErrorMessage = "{0}最少为3个字")]
        [MaxLength(20, ErrorMessage = "{0}最多为10个字")]
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "内容")]
        public string Content { get; set; }

        [Display(Name = "草稿?")]
        public bool IsDraft { get; set; }
    }

    public class MailMenuModel
    {
        public int ReceiedNewCount { set; get; }
        public int DraftCount { set; get; }
        public int SystemNewCount { set; get; }
        public int PrivateNewCount { set; get; }


    }
}