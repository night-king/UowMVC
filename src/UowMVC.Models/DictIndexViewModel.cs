using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class DictIndexViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "唯一编号")]
        public string No
        {
            set; get;
        }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "名称")]
        public string Name
        {
            set; get;
        }

        public virtual string ParentID { get; set; }

        public virtual string ParentName { get; set; }
        public DateTime CreateAt { get; set; }

        public DictIndexViewModel()
        {
        }

        public DictIndexViewModel(DictIndex entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            No = entity.No;
            ParentID = entity.Parent == null ? "" : entity.Parent.Id;
            ParentName = entity.Parent == null ? "" : entity.Parent.Name;
            CreateAt = entity.CreateAt;

        }
    }
}
