using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class NewsViewRecordViewModel
    {
        public string Id { get; set; }

        public string NewsId { get; set; }
        public string NewsTitle { get; set; }

        public string StudentNO { get; set; }
        public  string StudentId
        {
            set; get;
        }
        public NewsViewRecordViewModel() { }

        public NewsViewRecordViewModel(NewsViewRecord entity)
        {

            Id = entity.Id;
            NewsId = entity.NewsId;
            NewsTitle = entity.NewsTitle;
            StudentNO = entity.StudentNO;
            StudentId = entity.StudentId;
        }

    }
}
