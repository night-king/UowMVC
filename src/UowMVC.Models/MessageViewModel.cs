using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class MessageViewModel
    {
        public string Id { get; set; }
        public virtual UserViewModel Sender { get; set; }

        public virtual UserViewModel Accepter { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Status { get; set; }

        public int Type { get; set; }

        public DateTime CreateAt { get; set; }

        public MessageViewModel()
        {
        }

        public MessageViewModel(Message entity)
        {
            Id = entity.Id;
            Sender = entity.Sender == null ? null : new UserViewModel(entity.Sender);
            Accepter = entity.Accepter == null ? null : new UserViewModel(entity.Accepter);
            Title = entity.Title;
            Content = entity.Content;
            Status = (int)entity.Status;
            Type = (int)entity.Type;
            CreateAt = entity.CreateAt;
        }
    }
}
