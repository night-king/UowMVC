using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface IDepartmentService
    {
        bool Add(DepartmentViewModel model);

        bool Update(DepartmentViewModel model);

        DepartmentViewModel GetById(object id);

        bool Delete(object id);

        IEnumerable<DepartmentViewModel> GetAll();

        IEnumerable<DepartmentViewModel> Query(string key, int offset, int limit, out int count);

    }
}
