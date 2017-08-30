using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public class Configuration : BaseEntity
    {
        [StringLength(128, MinimumLength = 0)]
        public string Key { set; get; }
       
        public string Value { set; get; }

        public int No { set; get; }

        public ConfigurationTypeEnum Type { set; get; }

    }
}
