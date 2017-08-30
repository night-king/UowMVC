using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface INewsCategoryService
    {
        bool Add(NewsCategoryViewModel model);

        bool Update(NewsCategoryViewModel model);
        bool Delete(object id);

        NewsCategoryViewModel GetById(object id);

        IEnumerable<NewsCategoryViewModel> GetAll();
    }
}
