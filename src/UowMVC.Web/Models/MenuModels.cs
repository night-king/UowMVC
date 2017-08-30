using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UowMVC.Web.Models
{
    public class MenuModel
    {
        public ICollection<MenuItem> Items { get; set; }

        public MenuModel()
        {
            Items = new HashSet<MenuItem>();
        }
    }
    public class NavgatorModel
    {
        public MenuItem CurrentMenuModel { get; set; }
        public MenuItem CurrentPageModel { get; set; }
        public MenuItem CurrentButtonModel { get; set; }
    }

    public class MenuItem
    {
        public string Id { get; set; }
        public string Icon { get; set; }
        public string Text { get; set; }
        public string Action { get; set; }
        public string RelevantURL { get; set; }

        public int OpenStyle { get; set; }
        public bool IsMustSelected { get; set; }
        public bool IsDisplayOnTable { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsActive { get; set; }

        public ICollection<MenuItem> Items { get; set; }

        public MenuItem(string id, string icon, string text, string action, string relevantURL, int openstyle, bool isMustSelected,bool isDisplayOnTable, int width, int height, bool isActive = false)
        {
            Id = id;
            Icon = icon;
            Text = text;
            Action = action;
            RelevantURL = relevantURL;
            Items = new HashSet<MenuItem>();
            OpenStyle = openstyle;
            IsMustSelected = isMustSelected;
            IsDisplayOnTable = isDisplayOnTable;
            Width = width;
            Height = height;
            IsActive = isActive;
        }
    }
}