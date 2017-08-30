using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface IUserGroupService
    {
        bool Add(UserGroupViewModel model);

        bool Update(UserGroupViewModel model);

        UserGroupViewModel GetById(object id);

        bool Delete(object id);

        IEnumerable<UserGroupViewModel> GetAll();

    }
}
