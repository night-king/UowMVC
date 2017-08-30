using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class ListViewModel<T>
    {
        public List<T> Items { get; set; }
        public ListViewModel()
        {
            Items = new List<T>();
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Keyword { get; set; }
        public int TotalNumber { get; set; }
    }
}
