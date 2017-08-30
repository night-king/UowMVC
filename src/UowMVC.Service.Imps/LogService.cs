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
    public class LogService : ServiceBase, ILogService
    {
        public LogService(DefaultDataContext dbcontext):base(dbcontext)
        {
        }

        public LogViewModel GetById(string id)
        {
            var entity = uow.Set<Log>().Find(id);
            if (entity == null)
                return null;
            return new LogViewModel(entity);
        }

        public IEnumerable<LogViewModel> Query(string key, int offset, int limit, out int count, string level = null)
        {
            var query = uow.Set<Log>().AsQueryable();
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.UserIP.Contains(key)|| x.UserName.Contains(key) || x.Message.Contains(key));
            }
            if (!string.IsNullOrEmpty(level))
            {
                query = query.Where(x => x.Level == level);
            }
            count = query.Count();
            return query.OrderByDescending(x => x.CreateAt).Skip(offset).Take(limit).ToList().Select(x => new LogViewModel(x));

        }

        public void Report(string id)
        {
            var log = uow.Set<Log>().Find(id);
            if (log != null && log.Status != LogStatusEnum.Reported && log.Status != LogStatusEnum.Resolved)
            {
                log.Status = LogStatusEnum.Reported;
                uow.Commit();
            }
        }

        public void Resolve(string id)
        {
            var log = uow.Set<Log>().Find(id);
            if (log != null && log.Status != LogStatusEnum.Resolved)
            {
                log.Status = LogStatusEnum.Resolved;
                uow.Commit();
            }
        }
    }
}
