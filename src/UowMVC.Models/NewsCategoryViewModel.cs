using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class NewsCategoryViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "序号")]
        public int No { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "名称")]
        public string Name { get; set; }

        public NewsCategoryViewModel() { }
        public NewsCategoryViewModel(NewsCategory entity)
        {
            Id = entity.Id;
            No = entity.No;
            Name = entity.Name;
        }

    }
}
