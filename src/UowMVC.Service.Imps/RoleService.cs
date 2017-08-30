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
    public class RoleService : ServiceBase, IRoleService
    {
        public RoleService(DefaultDataContext dbcontext):base(dbcontext)
        {
        }
        public bool Add(RoleViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            ApplicationRole entity = new ApplicationRole();
            uow.Set<ApplicationRole>().Add(entity);
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Description = model.Description;
            uow.Commit();
            return true;
        }

        public bool Delete(object id)
        {
            var entity = uow.Set<ApplicationRole>().Find(id);
            if (entity == null)
                return false;
            uow.Set<ApplicationRole>().Remove(entity);
            uow.Commit();
            return true;
        }

        public RoleViewModel GetById(object id)
        {
            var entity = uow.Set<ApplicationRole>().Find(id);
            if (entity == null)
                return null;
            return new RoleViewModel(entity);
        }

        public IEnumerable<RolePermissionViewModel> GetPermission(string roleId)
        {
            return uow.Set<RolePermission>().Where(x => x.Role.Id == roleId).ToList().Select(x => new RolePermissionViewModel(x));
        }

        public void SavePermission(string roleId, IEnumerable<RolePermissionViewModel> permissions)
        {
            var exist = uow.Set<RolePermission>().Where(x => x.Role.Id == roleId).ToList();
            if (exist != null && exist.Count > 0)
            {
                uow.Remove(exist);
            }
            var role = uow.Set<ApplicationRole>().Find(roleId);
            var rolePermissions = new List<RolePermission>();
            foreach (var per in permissions)
            {
                var menu = uow.Set<Menu>().Find(per.MenuID);
                rolePermissions.Add(new RolePermission
                {
                    IsChecked = true,
                    Menu = menu,
                    Role = role
                });
            }
            uow.Set<RolePermission>().AddRange(rolePermissions.ToArray());
            uow.Commit();
        }

        public IEnumerable<RoleViewModel> Query(string key, int offset, int limit, out int count)
        {
            var query = uow.Set<ApplicationRole>().AsQueryable();
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Name.Contains(key));
            }
            count = query.Count();
            return query.OrderByDescending(x => x.CreateAt).Skip(offset).Take(limit).ToList().Select(x => new RoleViewModel(x));
        }



        public bool Update(RoleViewModel model)
        {
            var entity = uow.Set<ApplicationRole>().Find(model.Id);

            if (entity == null)
                return false;
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Type = (ApplicationRoleTypeEnum)model.Type;
            uow.Commit();
            return true;
        }

        public IEnumerable<RoleViewModel> GetAll()
        {
            return uow.Set<ApplicationRole>().ToList().Select(x => new RoleViewModel(x));
        }
    }
}
