using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public abstract class BaseEntity
    {
        [StringLength(128, MinimumLength = 0)]
        public string Id { get; set; }

        public DateTime CreateAt { get; set; }

        public BaseEntity()
        {
            CreateAt = DateTime.Now;
            Id = Guid.NewGuid().ToString();
        }
    }
}
