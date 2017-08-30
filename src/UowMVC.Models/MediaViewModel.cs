using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class MediaViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }
        public long Size { get; set; }

        public string RelavtivePath { get; set; }

        public string AbsolutePath
        {
            get; set;
        }
        public string Extension { get; set; }
        public string ResourceDomain { get; set; }

        public MediaViewModel()
        {
        }

        public MediaViewModel(Media entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Type = (int)entity.Type;
            Extension = entity.Extension;
            Size = entity.Size;
            RelavtivePath = entity.RelavtivePath;
            ResourceDomain = entity.ResourceDomain;
            AbsolutePath = entity.AbsolutePath;
        }
    }
}
