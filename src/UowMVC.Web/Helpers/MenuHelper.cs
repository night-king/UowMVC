using UowMVC.Web.Models;
using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UowMVC.Web.Helpers
{
    public class MenuHelper
    {
        public static MenuModel Convert(IEnumerable<MenuViewModel> viewModel, string url = "")
        {
            var menuModel = new MenuModel();
            if (viewModel != null && viewModel.Count() > 0)
            {
                var count = viewModel.Count();
                var roots = viewModel.Where(x => string.IsNullOrEmpty(x.ParentID)).ToList();
                if (!roots.Any())
                {
                    for (int i = 0; i < count; i++)
                    {
                        var current = viewModel.ElementAt(i);
                        if (!viewModel.Any(x => x.ParentID == current.Id))
                        {
                            roots.Add(current);
                        }
                    }
                }
                foreach (var menu in roots.OrderBy(x => x.No))
                {
                    var isActived = menu.URL != null && (menu.URL.ToLower() == url.ToLower() || (url.ToLower() != "/" && url.ToLower() != "/home/index" && menu.RelevantURL != null && menu.RelevantURL.ToLower().Contains(url.ToLower())));
                    var rootMenu = new MenuItem(menu.Id, menu.Icon, menu.Name, menu.URL, menu.RelevantURL, menu.OpenStyle, menu.IsMustSelected, menu.IsDisplayOnTable, menu.Width, menu.Height, isActived);
                    menuModel.Items.Add(rootMenu);
                    appendChildren(menuModel, viewModel, rootMenu, url);
                }
            }
            foreach (var root in menuModel.Items)
            {
                root.IsActive = root.Items.Any(x => x.IsActive);
            }
            return menuModel;
        }
        private static void appendChildren(MenuModel menuModel, IEnumerable<MenuViewModel> viewModel, MenuItem parent, string url = "")
        {
            foreach (var child in viewModel.Where(x => x.ParentID == parent.Id).OrderBy(x => x.No))
            {
                var isActived = child.URL.ToLower() == url.ToLower() || (url.ToLower() != "/" && url.ToLower() != "/home/index" && child.RelevantURL != null && child.RelevantURL.ToLower().Contains(url.ToLower()));
                var childMenu = new MenuItem(child.Id, child.Icon, child.Name, child.URL, child.RelevantURL, child.OpenStyle, child.IsMustSelected, child.IsDisplayOnTable, child.Width, child.Height, isActived);
                if (childMenu.IsActive)
                {
                    parent.IsActive = true;
                }
                parent.Items.Add(childMenu);
                appendChildren(menuModel, viewModel, childMenu, url);
            }
        }

    }
}