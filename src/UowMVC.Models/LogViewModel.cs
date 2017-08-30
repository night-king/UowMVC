using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class LogViewModel
    {
        public string Id { set; get; }
        public string Thread { set; get; }
        public string Level { set; get; }
        public string Logger { set; get; }
        public string UserIP { set; get; }

        public string URL { set; get; }

        public string UserName { set; get; }
        public string Action { get; set; }

        public string Client { set; get; }
        public string StatusCode { set; get; }
        public string Message { set; get; }
        public string Exception { set; get; }
        public LogStatusEnum Status { set; get; }
        public DateTime CreateAt { set; get; }
        public LogViewModel()
        {
        }

        public LogViewModel(Log entity)
        {
            Id = entity.Id;
            Thread = entity.Thread;
            Level = entity.Level;
            Logger = entity.Logger;

            UserIP = entity.UserIP;
            URL = entity.URL;
            UserName = entity.UserName;
            Action = entity.Action;
            Client = entity.Client;
            StatusCode = entity.StatusCode;
            Status = entity.Status;
            Message = entity.Message;
            Exception = entity.Exception;
            CreateAt = entity.CreateAt;
        }
    }
}
