using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class RolePermissionViewModel
    {

        public string MenuID { get; set; }

        public int No { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public string ParentID { get; set; }

        public bool IsChecked { get; set; }

        public string URL { get; set; }

        public RolePermissionViewModel()
        {
        }

        public RolePermissionViewModel(RolePermission entity)
        {

            if (entity.Menu != null)
            {
                MenuID = entity.Menu.Id;
                Name = entity.Menu.Name;
                No = entity.Menu.No;
                Icon = entity.Menu.Icon;
                ParentID = entity.Menu.Parent == null ? "" : entity.Menu.Parent.Id;
                URL = entity.Menu.URL;
            }

            IsChecked = entity.IsChecked;
        }
        public RolePermissionViewModel(MenuViewModel entity, bool isChecked)
        {
            MenuID = entity.Id;
            Name = entity.Name;
            No = entity.No;
            Icon = entity.Icon;
            ParentID = entity.ParentID;
            URL = entity.URL;
            IsChecked = isChecked;
        }
    }
}
