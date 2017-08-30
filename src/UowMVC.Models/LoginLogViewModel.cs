using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class LoginLogViewModel
    {
        public string Id { get; set; }

        public DateTime CreateAt { get; set; }

        public string IP { set; get; }
        public string Client { set; get; }

        public string Place { set; get; }
        public bool Result { set; get; }

        public string Message { set; get; }

        public string UserName { set; get; }

        public LoginLogViewModel()
        {
        }

        public LoginLogViewModel(LoginLog entity)
        {
            Id = entity.Id;
            CreateAt = entity.CreateAt;
            IP = entity.IP;
            Place = entity.Place;
            Message = entity.Message;
            Client = entity.Client;
            UserName = entity.UserName;
            Result = entity.Result;
        }
    }
}
