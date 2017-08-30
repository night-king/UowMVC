using UowMVC.Repository;
using UowMVC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UowMVC.Models;
using UowMVC.Domain;

namespace UowMVC.Service.Imps
{
    public class NewsCategoryService : ServiceBase, INewsCategoryService
    {
        public NewsCategoryService(DefaultDataContext dbcontext) : base(dbcontext)
        {
        }

        public bool Add(NewsCategoryViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            NewsCategory entity = new NewsCategory();
            uow.Set<NewsCategory>().Add(entity);
            entity.Id = model.Id;
            entity.No = model.No;

            entity.Name = model.Name;
            uow.Commit();
            return true;
        }

        public bool Delete(object id)
        {
            NewsCategory entity = uow.Set<NewsCategory>().Find(id);
            if (entity == null)
                return false;
            uow.Set<NewsCategory>().Remove(entity);
            uow.Commit();
            return true;
        }

        public IEnumerable<NewsCategoryViewModel> GetAll()
        {
            return uow.Set<NewsCategory>().OrderBy(x => x.CreateAt).ToList().Select(x => new NewsCategoryViewModel(x));
        }

        public NewsCategoryViewModel GetById(object id)
        {
            var entity = uow.Set<NewsCategory>().Find(id);
            if (entity == null)
                return null;
            return new NewsCategoryViewModel(entity);
        }

        public bool Update(NewsCategoryViewModel model)
        {
            var entity = uow.Set<NewsCategory>().Find(model.Id);
            if (entity == null)
                return false;
            entity.No = model.No;
            entity.Name = model.Name;
            uow.Commit();
            return true;
        }
    }
}
