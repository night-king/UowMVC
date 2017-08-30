using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface IRoleService
    {
        bool Add(RoleViewModel model);

        bool Update(RoleViewModel model);

        RoleViewModel GetById(object id);

        bool Delete(object id);

        IEnumerable<RoleViewModel> GetAll();

        IEnumerable<RoleViewModel> Query(string key, int offset, int limit, out int count);

        void SavePermission(string roleId, IEnumerable<RolePermissionViewModel> permissions);

        IEnumerable<RolePermissionViewModel> GetPermission(string roleId);
    }
}
