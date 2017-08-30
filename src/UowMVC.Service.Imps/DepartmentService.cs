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
    public class DepartmentService : ServiceBase, IDepartmentService
    {
        public DepartmentService(DefaultDataContext dbcontext):base(dbcontext)
        {
        }

        public bool Add(DepartmentViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            Department entity = new Department();
            uow.Set<Department>().Add(entity);
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.No = model.No;
            entity.Parent = uow.Set<Department>().Find(model.ParentID);
            uow.Commit();
            return true;
        }

        public bool Delete(object id)
        {
            Department entity = uow.Set<Department>().Find(id);
            if (entity == null)
                return false;
            uow.Set<Department>().Remove(entity);
            uow.Commit();
            return true;
        }

        public IEnumerable<DepartmentViewModel> GetAll()
        {
            return uow.Set<Department>().ToList().Select(x => new DepartmentViewModel(x));
        }

        public DepartmentViewModel GetById(object id)
        {
            var entity = uow.Set<Department>().Find(id);
            if (entity == null)
                return null;
            return new DepartmentViewModel(entity);
        }

        public IEnumerable<DepartmentViewModel> Query(string key, int offset, int limit, out int count)
        {
            var query = uow.Set<Department>().AsQueryable();
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Name.Contains(key));
            }
            count = query.Count();
            return query.OrderBy(x => x.No).Skip(offset).Take(limit).ToList().Select(x => new DepartmentViewModel(x));
        }

        public bool Update(DepartmentViewModel model)
        {
            var entity = uow.Set<Department>().Find(model.Id);

            if (entity == null)
                return false;

            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.No = model.No;
            entity.Parent = uow.Set<Department>().Find(model.ParentID);
            uow.Commit();
            return true;
        }
    }
}
