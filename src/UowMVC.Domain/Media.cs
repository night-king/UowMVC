using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{

    public class Media : BaseEntity
    {
        [StringLength(256, MinimumLength = 0)]
        public string Name { get; set; }

        [StringLength(256, MinimumLength = 0)]
        public string Extension { get; set; }

        public long Size { get; set; }

        public MediaTypeEnum Type { get; set; }

        [StringLength(2500, MinimumLength = 0)]
        public string RelavtivePath { get; set; }
        public string ResourceDomain { get; set; }
        public string AbsolutePath
        {
            get
            {
                if (Type == MediaTypeEnum.Image)
                {
                    return RelavtivePath.StartsWith("http") ? RelavtivePath : ResourceDomain + System.Configuration.ConfigurationManager.AppSettings["ImageAction"] + "/" + Id;
                }
                else
                {
                    return RelavtivePath.StartsWith("http") ? RelavtivePath : ResourceDomain + RelavtivePath;
                }
            }

        }

    }
}
