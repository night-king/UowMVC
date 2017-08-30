using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class FriendRelationshipViewModel
    {
        public string Id { set; get; }
        public virtual UserViewModel Owner { get; set; }

        public virtual UserViewModel User { get; set; }

        public int Status { get; set; }
        public DateTime CreateAt { get; set; }

        public FriendRelationshipViewModel()
        {
        }

        public FriendRelationshipViewModel(FriendRelationship ship)
        {
            Id = ship.Id;
            Owner = new UserViewModel(ship.Owner);
            User = new UserViewModel(ship.User);
            Status = (int)ship.Status;
            CreateAt = ship.CreateAt;
        }
    }
}
