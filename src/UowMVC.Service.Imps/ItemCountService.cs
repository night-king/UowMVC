using UowMVC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UowMVC.Models;
using UowMVC.Repository;
using UowMVC.Domain;

namespace UowMVC.Service.Imps
{
    public class ItemCountService : ServiceBase, IItemCountService
    {
        public ItemCountService(DefaultDataContext dbcontext) : base(dbcontext)
        {

        }

        public IEnumerable<ItemCountViewModel> GetHome()
        {
            var result = new List<ItemCountViewModel>();
            return result;
        }
    }
}
