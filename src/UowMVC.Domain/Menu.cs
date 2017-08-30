using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public class Menu : BaseEntity
    {
        public int No { get; set; }

        /// <summary>
        /// 是否是控制面板项目
        /// </summary>
        public bool IsControlPanel { get; set; }

        /// <summary>
        /// 是否在表格列表页显示
        /// </summary>
        public bool IsDisplayOnTable { get; set; }

        [StringLength(128, MinimumLength = 0)]
        public string Name { get; set; }
        [StringLength(128, MinimumLength = 0)]
        public string Icon { get; set; }

        public MenuOpenStyleEnum OpenStyle { get; set; }

        /// <summary>
        /// 主URL
        /// </summary>
        [StringLength(1000, MinimumLength = 0)]
        public string URL { get; set; }

        /// <summary>
        /// 关联URL：
        /// 访问关联URL的授权依赖“主URL"权限设置，
        /// 通常用于完成在一个页面上完成复杂功能(例如ajax调用)，将此功能捆绑成一个权限。
        /// </summary>
        [StringLength(1000, MinimumLength = 0)]
        public string RelevantURL { get; set; }

        [StringLength(1000, MinimumLength = 0)]
        public string Description { get; set; }

        public bool IsMustSelected { get; set; }

        public virtual Menu Parent { get; set; }

        /// <summary>
        /// OpenStyle=Dialog 窗口宽度，OpenStyle=Page时不填写
        /// </summary>
        public virtual int Width { get; set; }

        /// <summary>
        ///  OpenStyle=Dialog 窗口高度，OpenStyle=Page时不填写
        /// </summary>
        public virtual int Height { get; set; }

    }

    public enum MenuOpenStyleEnum
    {
        [Description("Dialog")]
        Dialog = 1,

        [Description("Page")]
        Page = 2
    }
}
