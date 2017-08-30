using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UowMVC.Web.Models
{
    public class ImportModel
    {

        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "文件")]
        public string File { get; set; }
    }
}