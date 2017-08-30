using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    [Serializable]
    public class MenuViewModel
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


        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "图标")]
        public string Icon { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "打开方式")]
        public int OpenStyle { get; set; }

        [Display(Name = "必须选中")]
        public bool IsMustSelected { get; set; }

        [Display(Name = "URL")]
        public string URL { get; set; }

        [Display(Name = "关联URL")]
        public string RelevantURL { get; set; }

        [Display(Name = "描述")]
        public string Description { get; set; }

        public string ParentID { get; set; }

        public string ParentName { get; set; }

        [Display(Name = "窗口宽度")]
        public int Width { get; set; }
        [Display(Name = "窗口高度")]
        public int Height { get; set; }

        [Display(Name = "是否受控")]
        public bool IsControlPanel { get; set; }

        /// <summary>
        /// 是否在表格列表页显示
        /// </summary>
        [Display(Name = "是否显示")]
        public bool IsDisplayOnTable { get; set; }

        public MenuViewModel()
        {
        }

        public MenuViewModel(Menu entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            No = entity.No;
            Icon = entity.Icon;
            OpenStyle = (int)entity.OpenStyle;
            URL = entity.URL;
            Description = entity.Description;
            ParentID = entity.Parent == null ? "" : entity.Parent.Id;
            ParentName = entity.Parent == null ? "" : entity.Parent.Name;
            IsMustSelected = entity.IsMustSelected;
            Width = entity.Width;
            RelevantURL = entity.RelevantURL;
            Height = entity.Height;
            IsDisplayOnTable = entity.IsDisplayOnTable;
            IsControlPanel = entity.IsControlPanel;
        }

    }
}
