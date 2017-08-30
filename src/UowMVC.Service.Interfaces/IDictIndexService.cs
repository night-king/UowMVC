using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface IDictIndexService
    {
        bool Add(DictIndexViewModel model);

        bool Update(DictIndexViewModel model);

        DictIndexViewModel GetById(object id);

        bool Delete(object id);

        IEnumerable<DictIndexViewModel> GetAll();


    }
}
