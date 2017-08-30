using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public class FriendRelationship : BaseEntity
    {
        public virtual ApplicationUser Owner { get; set; }

        public virtual ApplicationUser User { get; set; }

        public FriendRelationshipStatusEnum Status { get; set; }
    }
}
