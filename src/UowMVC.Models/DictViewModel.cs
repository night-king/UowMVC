using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class DictViewModel
    {
        public string Id { get; set; }


        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "序号")]
        public string No
        {
            set; get;
        }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "字典键")]
        public string Key
        {
            set; get;
        }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "字典值")]
        public string Value
        {
            set; get;
        }
        [Display(Name = "描述")]
        public string Description
        {
            set; get;
        }

        public virtual string IndexID { get; set; }

        public virtual string IndexName { get; set; }
        public DateTime CreateAt { get; set; }

        public DictViewModel()
        {
        }

        public DictViewModel(Dict entity)
        {
            Id = entity.Id;
            No = entity.No;
            Key = entity.Key;
            Value = entity.Value;
            IndexID = entity.Index == null ? "" : entity.Index.Id;
            IndexName = entity.Index == null ? "" : entity.Index.Name;
            CreateAt = entity.CreateAt;
            Description = entity.Description;
        }
    }
}
