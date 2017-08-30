using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class ConfigurationViewModel
    {
        public string Id { set; get; }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "项目名称")]
        public string Key { set; get; }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "项目值")]
        public string Value { set; get; }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "序号")]
        public int No { set; get; }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "类型")]
        public int Type { set; get; }

        public ConfigurationViewModel()
        {
        }

        public ConfigurationViewModel(Configuration entity)
        {
            Id = entity.Id;
            No = entity.No;
            Key = entity.Key;
            Value = entity.Value;
            Type = (int)entity.Type;
        }
    }
}
