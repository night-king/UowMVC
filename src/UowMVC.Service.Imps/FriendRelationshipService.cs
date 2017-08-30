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
    public class FriendRelationshipService : ServiceBase, IFriendRelationshipService
    {
        public FriendRelationshipService(DefaultDataContext dbcontext) : base(dbcontext)
        {

        }

        public bool Add(FriendRelationshipViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            FriendRelationship entity = new FriendRelationship();
            uow.Set<FriendRelationship>().Add(entity);
            entity.Id = model.Id;
            entity.User = model.User == null ? null : uow.Set<ApplicationUser>().Find(model.User.Id);
            entity.Owner = model.Owner == null ? null : uow.Set<ApplicationUser>().Find(model.Owner.Id);
            entity.Status = (FriendRelationshipStatusEnum)model.Status;
            uow.Commit();
            return true;
        }

        public bool Delete(object id)
        {
            FriendRelationship entity = uow.Set<FriendRelationship>().Find(id);
            if (entity == null)
                return false;
            uow.Set<FriendRelationship>().Remove(entity);
            uow.Commit();
            return true;
        }

        public FriendRelationshipViewModel GetById(object id)
        {
            var entity = uow.Set<FriendRelationship>().Find(id);
            if (entity == null)
                return null;
            return new FriendRelationshipViewModel(entity);
        }

        public IEnumerable<FriendRelationshipViewModel> Query(string key, int offset, int limit, out int count, string ownerId = "")
        {
            var query = uow.Set<FriendRelationship>().AsQueryable();
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.User.UserName.Contains(key) || x.User.Name.Contains(key));
            }
            if (!string.IsNullOrEmpty(ownerId))
            {
                query = query.Where(x => x.Owner.Id == ownerId);
            }
            count = query.Count();
            return query.OrderByDescending(x => x.CreateAt).Skip(offset).Take(limit).ToList().Select(x => new FriendRelationshipViewModel(x));
        }

        public bool Update(FriendRelationshipViewModel model)
        {
            var entity = uow.Set<FriendRelationship>().Find(model.Id);
            if (entity == null)
                return false;
            entity.Id = model.Id;
            entity.User = model.User == null ? null : uow.Set<ApplicationUser>().Find(model.User.Id);
            entity.Owner = model.Owner == null ? null : uow.Set<ApplicationUser>().Find(model.Owner.Id);
            entity.Status = (FriendRelationshipStatusEnum)model.Status;
            uow.Commit();
            return true;
        }
    }
}
