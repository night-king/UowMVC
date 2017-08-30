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
    public class LoginLogService : ServiceBase, ILoginLogService
    {
        public LoginLogService(DefaultDataContext dbcontext):base(dbcontext)
        {
        }

        public bool Add(LoginLogViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            LoginLog entity = new LoginLog();
            uow.Set<LoginLog>().Add(entity);
            entity.Id = model.Id;
            entity.IP = model.IP;
            entity.Place = model.Place;
            entity.Result = model.Result;
            entity.UserName = model.UserName;
            entity.Message = model.Message;
            uow.Commit();
            return true;
        }

        public IEnumerable<LoginLogViewModel> Query(string key, int offset, int limit, out int count, int result = -1)
        {
            var query = uow.Set<LoginLog>().AsQueryable();
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.IP.Contains(key) || x.UserName.Contains(key) || x.Message.Contains(key));
            }
            if (result >= 0)
            {
                var isSuccess = result > 0;
                query = query.Where(x => x.Result == isSuccess);
            }
            count = query.Count();
            return query.OrderByDescending(x => x.CreateAt).Skip(offset).Take(limit).ToList().Select(x => new LoginLogViewModel(x));

        }

        public IEnumerable<LoginLogViewModel> QueryByUser(string key, string username, int offset, int limit, out int count, int result = -1)
        {
            var query = uow.Set<LoginLog>().Where(x => x.UserName == username);
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.IP.Contains(key) || x.UserName.Contains(key) || x.Message.Contains(key));
            }
            if (result >= 0)
            {
                var isSuccess = result > 0;
                query = query.Where(x => x.Result == isSuccess);
            }
            count = query.Count();
            return query.OrderByDescending(x => x.CreateAt).Skip(offset).Take(limit).ToList().Select(x => new LoginLogViewModel(x));

        }
    }
}
