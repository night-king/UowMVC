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
    public class MessageService : ServiceBase, IMessageService
    {
        public MessageService(DefaultDataContext dbcontext) : base(dbcontext)
        {
        }

        public bool Add(MessageViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            Message entity = new Message();
            uow.Set<Message>().Add(entity);
            entity.Id = model.Id;
            entity.Accepter = model.Accepter == null ? null : uow.Set<ApplicationUser>().Find(model.Accepter.Id);
            entity.Sender = model.Sender == null ? null : uow.Set<ApplicationUser>().Find(model.Sender.Id);
            entity.Status = (MessageStatusEnum)model.Status;
            entity.Type = (MessageTypeEnum)model.Type;
            entity.Title = model.Title;
            entity.Content = model.Content;

            uow.Commit();
            return true;
        }

        public bool Delete(object id)
        {
            Message entity = uow.Set<Message>().Find(id);
            if (entity == null || entity.IsDelete)
                return false;
            entity.IsDelete = true;
            uow.Commit();
            return true;
        }
        public bool Remove(object id)
        {
            Message entity = uow.Set<Message>().Find(id);
            if (entity == null)
                return false;
            uow.Set<Message>().Remove(entity);
            uow.Commit();
            return true;
        }
        public bool Read(object id)
        {
            Message entity = uow.Set<Message>().Find(id);
            if (entity == null || entity.IsDelete)
                return false;
            entity.Status = MessageStatusEnum.Readed;
            uow.Commit();
            return true;
        }
        public bool UnRead(object id)
        {
            Message entity = uow.Set<Message>().Find(id);
            if (entity == null || entity.IsDelete)
                return false;
            entity.Status = MessageStatusEnum.New;
            uow.Commit();
            return true;
        }
        public MessageViewModel GetById(object id, bool isReaded = false)
        {
            var entity = uow.Set<Message>().Find(id);
            if (entity == null || entity.IsDelete)
                return null;
            if (isReaded && entity.Status == MessageStatusEnum.New)
            {
                entity.Status = MessageStatusEnum.Readed;
                uow.Commit();
            }
            return new MessageViewModel(entity);
        }

        public IEnumerable<MessageViewModel> Query(string key, int offset, int limit, out int count, string ownerId = "")
        {

            var query = uow.Set<Message>().Where(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Title.Contains(key) || x.Accepter.UserName.Contains(key) || x.Accepter.Name.Contains(key));
            }
            if (!string.IsNullOrEmpty(ownerId))
            {
                query = query.Where(x => x.Accepter.Id == ownerId || x.Sender.Id == ownerId);
            }
            count = query.Count();
            return query.OrderByDescending(x => x.CreateAt).Skip(offset).Take(limit).ToList().Select(x => new MessageViewModel(x));

        }

        public int QueryAcceptCount(string userId, MessageStatusEnum status, MessageTypeEnum type)
        {
            var query = uow.Set<Message>().Where(x => x.IsDelete == false);
            return query.Where(x => x.IsDelete == false && x.Accepter.Id == userId && (x.Status & status) != 0 && (x.Type & type) != 0).Count();
        }

        public int QuerySendCount(string userId, MessageStatusEnum status, MessageTypeEnum type)
        {
            var query = uow.Set<Message>().Where(x => x.IsDelete == false);
            return query.Where(x => x.Sender.Id == userId && (x.Status & status) != 0 && (x.Type & type) != 0).Count();
        }

        public IEnumerable<MessageViewModel> QueryReceiveNew(string userName, out int count)
        {
            var query = uow.Set<Message>().Where(x => x.IsDelete == false && x.Status == MessageStatusEnum.New);
            if (!string.IsNullOrEmpty(userName))
            {
                query = query.Where(x => x.Accepter.UserName == userName);
            }
            count = query.Count();
            return query.OrderByDescending(x => x.CreateAt).Skip(0).Take(10).ToList().Select(x => new MessageViewModel(x));
        }



        public bool Update(MessageViewModel model)
        {
            var entity = uow.Set<Message>().Find(model.Id);
            if (entity == null || entity.IsDelete)
                return false;
            entity.Id = model.Id;
            entity.Accepter = model.Accepter == null ? null : uow.Set<ApplicationUser>().Find(model.Accepter.Id);
            entity.Sender = model.Sender == null ? null : uow.Set<ApplicationUser>().Find(model.Sender.Id);
            entity.Status = (MessageStatusEnum)model.Status;
            entity.Type = (MessageTypeEnum)model.Type;
            entity.Content = model.Content;
            uow.Commit();
            return true;
        }

        public IEnumerable<MessageViewModel> QueryAccept(string key, int offset, int limit, out int count, MessageStatusEnum status, MessageTypeEnum type, string ownerId = "")
        {
            var query = uow.Set<Message>().Where(x => x.IsDelete == false && (x.Status & status) != 0 && (x.Type & type) != 0);
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Title.Contains(key) || x.Sender.UserName.Contains(key) || x.Sender.Name.Contains(key) || x.Content.Contains(key));
            }
            if (!string.IsNullOrEmpty(ownerId))
            {
                query = query.Where(x => x.Accepter.Id == ownerId);
            }
            count = query.Count();
            return query.OrderByDescending(x => x.CreateAt).Skip(offset).Take(limit).ToList().Select(x => new MessageViewModel(x));
        }

        public IEnumerable<MessageViewModel> QuerySend(string key, int offset, int limit, out int count, MessageStatusEnum status, MessageTypeEnum type, string ownerId = "")
        {
            var query = uow.Set<Message>().Where(x => x.IsDelete == false && (x.Status & status) != 0 && (x.Type & type) != 0);
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Title.Contains(key) || x.Accepter.UserName.Contains(key) || x.Accepter.Name.Contains(key) || x.Content.Contains(key));
            }
            if (!string.IsNullOrEmpty(ownerId))
            {
                query = query.Where(x => x.Sender.Id == ownerId);
            }
            count = query.Count();
            return query.OrderByDescending(x => x.CreateAt).Skip(offset).Take(limit).ToList().Select(x => new MessageViewModel(x));
        }

        public IEnumerable<MessageViewModel> QueryRecycle(string key, int offset, int limit, out int count, string ownerId = "")
        {
            var query = uow.Set<Message>().Where(x => x.IsDelete == true);
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Title.Contains(key) || x.Accepter.UserName.Contains(key) || x.Accepter.Name.Contains(key));
            }
            if (!string.IsNullOrEmpty(ownerId))
            {
                query = query.Where(x => x.Accepter.Id == ownerId || x.Sender.Id == ownerId);
            }
            count = query.Count();
            return query.OrderByDescending(x => x.CreateAt).Skip(offset).Take(limit).ToList().Select(x => new MessageViewModel(x));
        }
    }
}
