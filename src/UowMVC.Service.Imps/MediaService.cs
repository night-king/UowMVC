using UowMVC.Repository;
using UowMVC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UowMVC.Models;
using UowMVC.Domain;
using UowMVC.SDK;

namespace UowMVC.Service.Imps
{
    public class MediaService : ServiceBase, IMediaService
    {
        public MediaService(DefaultDataContext dbcontext) : base(dbcontext)
        {
        }

        public MediaViewModel Add(MediaViewModel model)
        {
            Media entity = new Media();
            entity.Id = Guid.NewGuid().ToString();
            uow.Set<Media>().Add(entity);
            entity.Name = model.Name;
            entity.Extension = model.Extension;
            entity.RelavtivePath = model.RelavtivePath;
            entity.ResourceDomain = model.ResourceDomain;
            entity.Type = (MediaTypeEnum)model.Type;
            uow.Commit();
            return new MediaViewModel(entity);
        }

        public bool Delete(object id)
        {
            Media entity = uow.Set<Media>().Find(id);
            if (entity == null)
                return false;
            uow.Set<Media>().Remove(entity);
            uow.Commit();
            return true;
        }

        public MediaViewModel GetById(object id)
        {
            var entity = uow.Set<Media>().Find(id);
            if (entity == null)
                return null;
            return new MediaViewModel(entity);
        }

        public IEnumerable<MediaViewModel> Query(string key, int offset, int limit, out int count)
        {
            var query = uow.Set<Media>().AsQueryable();
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Name.Contains(key));
            }
            count = query.Count();
            return query.OrderByDescending(x => x.CreateAt).Skip(offset).Take(limit).ToList().Select(x => new MediaViewModel(x));

        }
    }
}
