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
    public class DictIndexService : ServiceBase, IDictIndexService
    {
        public DictIndexService(DefaultDataContext dbcontext):base(dbcontext)
        {
        }

        public bool Add(DictIndexViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            DictIndex entity = new DictIndex();
            uow.Set<DictIndex>().Add(entity);
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.No = model.No;
            entity.Parent = uow.Set<DictIndex>().Find(model.ParentID);
            uow.Commit();
            return true;
        }

        public bool Delete(object id)
        {
            DictIndex entity = uow.Set<DictIndex>().Find(id);
            if (entity == null)
                return false;
            uow.Set<DictIndex>().Remove(entity);
            uow.Commit();
            return true;
        }

        public IEnumerable<DictIndexViewModel> GetAll()
        {
            return uow.Set<DictIndex>().ToList().Select(x => new DictIndexViewModel(x));
        }

        public DictIndexViewModel GetById(object id)
        {
            var entity = uow.Set<DictIndex>().Find(id);
            if (entity == null)
                return null;
            return new DictIndexViewModel(entity);
        }

        public bool Update(DictIndexViewModel model)
        {
            var entity = uow.Set<DictIndex>().Find(model.Id);

            if (entity == null)
                return false;

            entity.Name = model.Name;
            entity.No = model.No;
            entity.Parent = uow.Set<DictIndex>().Find(model.ParentID);
            uow.Commit();
            return true;
        }
    }
}
