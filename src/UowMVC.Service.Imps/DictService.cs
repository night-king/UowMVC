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
    public class DictService : ServiceBase, IDictService
    {
        public DictService(DefaultDataContext dbcontext) : base(dbcontext)
        {
        }

        public bool Add(DictViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            Dict entity = new Dict();
            uow.Set<Dict>().Add(entity);
            entity.Id = model.Id;
            entity.No = model.No;
            entity.Key = model.Key;
            entity.Value = model.Value;
            entity.Description = model.Description;

            entity.Index = uow.Set<DictIndex>().Find(model.IndexID);
            uow.Commit();
            return true;
        }

        public bool Delete(object id)
        {
            Dict entity = uow.Set<Dict>().Find(id);
            if (entity == null)
                return false;
            uow.Set<Dict>().Remove(entity);
            uow.Commit();
            return true;
        }

        public DictViewModel GetById(object id)
        {
            var entity = uow.Set<Dict>().Find(id);
            if (entity == null)
                return null;
            return new DictViewModel(entity);
        }

        public IEnumerable<DictViewModel> GetByIndexNo(string no)
        {
            var entity = uow.Set<Dict>().Where(x => x.Index.No == no).ToList();
            if (entity == null)
                return new List<DictViewModel>();
            return entity.Select(x => new DictViewModel(x));
        }

        public DictViewModel GetByKey(string key)
        {
            var entity = uow.Set<Dict>().FirstOrDefault(x => x.Key == key);
            if (entity == null)
            {
                return null;
            }
            return new DictViewModel(entity);
        }

        public IEnumerable<DictViewModel> Query(string key, int offset, int limit, out int count, string index = "")
        {
            var query = uow.Set<Dict>().AsQueryable();
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Key.Contains(key) || x.Value.Contains(key));
            }
            if (!string.IsNullOrEmpty(index))
            {
                query = query.Where(x => x.Index.Id == index);
            }
            count = query.Count();
            return query.OrderBy(x => x.No).Skip(offset).Take(limit).ToList().Select(x => new DictViewModel(x));


        }

        public bool Update(DictViewModel model)
        {
            var entity = uow.Set<Dict>().Find(model.Id);

            if (entity == null)
                return false;
            entity.No = model.No;
            entity.Key = model.Key;
            entity.Value = model.Value;
            entity.Description = model.Description;
            entity.Index = uow.Set<DictIndex>().Find(model.IndexID);
            uow.Commit();
            return true;
        }
    }
}
