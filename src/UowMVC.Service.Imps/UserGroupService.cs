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
    public class UserGroupService : ServiceBase, IUserGroupService
    {
        public UserGroupService(DefaultDataContext dbcontext):base(dbcontext)
        {
        }

        public bool Add(UserGroupViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            UserGroup entity = new UserGroup();
            uow.Set<UserGroup>().Add(entity);
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.No = model.No;
            uow.Commit();
            return true;
        }

        public bool Delete(object id)
        {
            UserGroup entity = uow.Set<UserGroup>().Find(id);
            if (entity == null)
                return false;
            uow.Set<UserGroup>().Remove(entity);
            uow.Commit();
            return true;
        }

        public IEnumerable<UserGroupViewModel> GetAll()
        {
            return uow.Set<UserGroup>().ToList().Select(x => new UserGroupViewModel(x));
        }

        public UserGroupViewModel GetById(object id)
        {
            var entity = uow.Set<UserGroup>().Find(id);
            if (entity == null)
                return null;
            return new UserGroupViewModel(entity);
        }

        public bool Update(UserGroupViewModel model)
        {
            var entity = uow.Set<UserGroup>().Find(model.Id);

            if (entity == null)
                return false;

            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.No = model.No;
            uow.Commit();
            return true;
        }
    }
}
