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
    public class NewsService : ServiceBase, INewsService
    {
        public NewsService(DefaultDataContext dbcontext) : base(dbcontext)
        {
        }

        public bool Add(NewsViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            News entity = new News();
            uow.Set<News>().Add(entity);
            entity.Id = model.Id;
            entity.Index = model.Index;
            entity.Title = model.Title;
            entity.Content = model.Content;
            entity.Year = model.Year;
            entity.Category = uow.Set<NewsCategory>().Find(model.CategoryId);
            uow.Commit();
            return true;
        }

        public bool Delete(object id)
        {
            News entity = uow.Set<News>().Find(id);
            if (entity == null)
                return false;
            uow.Set<News>().Remove(entity);
            uow.Commit();
            return true;
        }

        public NewsViewModel GetById(object id)
        {
            var entity = uow.Set<News>().Find(id);
            if (entity == null)
                return null;
            return new NewsViewModel(entity);
        }

        public IEnumerable<NewsViewModel> Query( string key, int offset, int limit, out int count, int year = 0, string categoryId = "")
        {
            var query = uow.Set<News>().AsQueryable();
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Title.Contains(key));
            }
            if (!string.IsNullOrEmpty(categoryId))
            {
                query = query.Where(x => x.Category.Id == categoryId);
            }
            if (year > 0) {
                query = query.Where(x => x.Year==year);

            }
            count = query.Count();
            return query.OrderBy(x => x.Index).Skip(offset).Take(limit).ToList().Select(x => new NewsViewModel(x));

        }

        public int QueryCount(int year)
        {
            return uow.Set<News>().Where(x => x.Year == year).Count();
        }

        public bool Update(NewsViewModel model)
        {
            var entity = uow.Set<News>().Find(model.Id);
            if (entity == null)
                return false;
            entity.Index = model.Index;
            entity.Title = model.Title;
            entity.Content = model.Content;
            entity.Year = model.Year;
            entity.Category = uow.Set<NewsCategory>().Find(model.CategoryId);
            uow.Commit();
            return true;
        }
    }
}
