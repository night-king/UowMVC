using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public class FakeDeleteEntity : BaseEntity
    {
        public bool IsDelete { get; set; }

        public DateTime? DeleteAt { get; set; }

        public ApplicationUser DeleteBy { get; set; }

        public FakeDeleteEntity()
        {
            IsDelete = false;
        }
    }
}
