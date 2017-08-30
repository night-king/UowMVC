using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface IMenuService
    {
        bool Add(MenuViewModel model);
        bool Verify(MenuViewModel model);

        bool Update(MenuViewModel model);

        MenuViewModel GetById(object id);

        bool Delete(object id);

        IEnumerable<MenuViewModel> GetAll();

        IEnumerable<MenuViewModel> GetByUrl(string url);

        IEnumerable<MenuViewModel> GetChildenByUrl(string url);

        IEnumerable<MenuViewModel> GetByRole(List<string> roleIds);

    }
}
