using UowMVC.Domain;
using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UowMVC.Web.Helpers
{
    public class ZTree
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public string ParentID { set; get; }

        public ZTree(string id, string name, string parentid)
        {
            Id = id;
            Name = name;
            ParentID = parentid;
        }

    }
    public class ZTreeHelper
    {
        public static List<znode> ToJson(IEnumerable<ZTree> datasource, string urlPrefix, string checkedId)
        {
            var znodes = new List<znode>();

            if (datasource == null)
            {
                return znodes;
            }
            foreach (var f in datasource.Where(x => string.IsNullOrEmpty(x.ParentID)))
            {
                var father = new znode
                {
                    id = f.Id,
                    isParent = true,
                    name = f.Name,
                    isSelected = checkedId == f.Id,
                    open = true,
                    target = "_self",
                    url = urlPrefix + (urlPrefix.IndexOf("?") >= 0 ? "&fid=" + f.Id : "?fid=" + f.Id),
                };
                recursive(datasource, father, urlPrefix, checkedId);
                znodes.Add(father);
            }
            return znodes;
        }

        public static void recursive(IEnumerable<ZTree> datasource, znode father, string urlPrefix, string checkedId)
        {
            var children = datasource.Where(x => x.ParentID == father.id);
            foreach (var child in children)
            {
                var ch = new znode
                {
                    id = child.Id,
                    isParent = false,
                    name = child.Name,
                    isSelected = checkedId == child.Id,
                    open = true,
                    target = "_self",
                    url = urlPrefix + (urlPrefix.IndexOf("?") >= 0 ? "&fid=" + child.Id : "?fid=" + child.Id),
                };
                father.children.Add(ch);
                recursive(datasource, ch, urlPrefix, checkedId);
            }
        }
    }
    public class znode
    {
        public string id { set; get; }
        public string name { set; get; }
        public List<znode> children { set; get; }
        public bool isParent { set; get; }
        public bool open { set; get; }
        public bool isSelected { set; get; }
        public string url { set; get; }
        public string target { set; get; }
        public znode()
        {
            open = true;
            children = new List<znode>();
        }

    }
}