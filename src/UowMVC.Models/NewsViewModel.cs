using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class NewsViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "年份")]
        public int Year { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "序号")]
        public int Index { get; set; }


        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "分类")]
        public string CategoryId { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "内容")]
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }

        public NewsCategoryViewModel Category { get; set; }

        public NewsViewModel() { }

        public NewsViewModel(News entity)
        {
            Id = entity.Id;
            Year = entity.Year;
            Title = entity.Title;
            Index = entity.Index;
            Content = entity.Content;
            CategoryId = entity.Category == null ? "" : entity.Category.Id;
            Category = entity.Category == null ? null : new NewsCategoryViewModel(entity.Category);
            CreateAt = entity.CreateAt;
        }

    }
}
