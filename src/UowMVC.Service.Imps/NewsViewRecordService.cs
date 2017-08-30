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
    public class NewsViewRecordService : ServiceBase, INewsViewRecordService
    {
        public NewsViewRecordService(DefaultDataContext dbcontext) : base(dbcontext)
        {
        }

        public bool Add(NewsViewRecordViewModel model)
        {
            News news = uow.Set<News>().Find(model.NewsId);
            if (news == null)
                return false;
            model.Id = Guid.NewGuid().ToString();
            NewsViewRecord entity = new NewsViewRecord();
            uow.Set<NewsViewRecord>().Add(entity);
            entity.Id = model.Id;
            entity.StudentNO = model.StudentNO;
            entity.StudentId = model.StudentId;
            entity.NewsId = model.NewsId;
            entity.NewsTitle = model.NewsTitle;
            entity.CreateAt = DateTime.Now;
            news.ViewCount++;
            uow.Commit();
            return true;
        }

        public bool Delete(object id)
        {
            NewsViewRecord entity = uow.Set<NewsViewRecord>().Find(id);
            if (entity == null)
                return false;
            uow.Set<NewsViewRecord>().Remove(entity);
            uow.Commit();
            return true;
        }

        public NewsViewRecordViewModel GetById(object id)
        {
            var entity = uow.Set<NewsViewRecord>().Find(id);
            if (entity == null)
                return null;
            return new NewsViewRecordViewModel(entity);
        }

        public IEnumerable<NewsViewRecordViewModel> Query(string key, int offset, int limit, out int count, string studentId = "")
        {
            var query = uow.Set<NewsViewRecord>().AsQueryable();
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.NewsTitle.Contains(key));
            }
            count = query.Count();
            return query.OrderBy(x => x.CreateAt).Skip(offset).Take(limit).ToList().Select(x => new NewsViewRecordViewModel(x));

        }

        public NewsViewRecordViewModel QueryByStudent(string newsId, string studentId)
        {
            var entity = uow.Set<NewsViewRecord>().FirstOrDefault(x => x.StudentId == studentId && x.NewsId == newsId);
            if (entity == null) return null;
            return new NewsViewRecordViewModel(entity);
        }

        public int QueryDistinctCountByStudent(string studentId)
        {
           return uow.Set<NewsViewRecord>().Where(x => x.StudentId == studentId).Select(x=>x.NewsId).Distinct().Count();
        }

        public bool Update(NewsViewRecordViewModel model)
        {
            var entity = uow.Set<NewsViewRecord>().Find(model.Id);
            if (entity == null)
                return false;
            entity.StudentNO = model.StudentNO;
            entity.StudentId = model.StudentId;
            entity.NewsId = model.NewsId;
            entity.NewsTitle = model.NewsTitle;
            uow.Commit();
            return true;
        }
    }
}
