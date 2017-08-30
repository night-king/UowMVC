using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public class Message : FakeDeleteEntity
    {
        public virtual ApplicationUser Sender { get; set; }

        public virtual ApplicationUser Accepter { get; set; }

        [StringLength(1000, MinimumLength = 0)]
        public string Title { get; set; }

        [StringLength(8000, MinimumLength = 0)]
        public string Content { get; set; }

        [StringLength(1000, MinimumLength = 0)]
        public string URL { get; set; }

        public MessageStatusEnum Status { get; set; }

        public MessageTypeEnum Type { get; set; }
    }
}
