using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class UserGroupViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "序号")]
        public string No { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "备注")]
        public string Description { get; set; }


        public UserGroupViewModel()
        {
        }

        public UserGroupViewModel(UserGroup entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            No = entity.No;
            Description = entity.Description;
          
        }

    }
}
