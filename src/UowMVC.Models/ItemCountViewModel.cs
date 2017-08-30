using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class ItemCountViewModel
    {
        public string Name { set; get; }

        public string Description { set; get; }

        public string Count { set; get; }

        public ItemCountViewModel() { }

        public ItemCountViewModel(string name, string desc, string count)
        {
            this.Name = name;
            this.Description = desc;
            this.Count = count;
        }
    }
}
