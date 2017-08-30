using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class RoleViewModel
    {
        public string Id { set; get; }

        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "名称")]
        public string Name { set; get; }

        [Display(Name = "类型")]
        public int Type { set; get; }

        [Display(Name = "描述")]
        public string Description { get; set; }

        public RoleViewModel()
        {
        }

        public RoleViewModel(ApplicationRole entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Type = (int)entity.Type;
            Description = entity.Description;

        }
    }
}
