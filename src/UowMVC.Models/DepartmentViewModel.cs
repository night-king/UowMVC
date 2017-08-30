using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UowMVC.Models
{
    public class DepartmentViewModel
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

        [Display(Name = "备注")]
        public string Description { get; set; }

        public virtual string ParentID { get; set; }

        public virtual string ParentName { get; set; }
        public IEnumerable<UserDepartmentRelationshipViewModel> Relationships { get; set; }


        public DepartmentViewModel()
        {
        }

        public DepartmentViewModel(Department entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            No = entity.No;
            Description = entity.Description;
            ParentID = entity.Parent == null ? "" : entity.Parent.Id;
            ParentName = entity.Parent == null ? "" : entity.Parent.Name;
            Relationships = entity.Relationships == null ? null : entity.Relationships.Select(x => new UserDepartmentRelationshipViewModel(x));
        }

    }
}
