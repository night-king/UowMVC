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
    public class QrcodeService : ServiceBase, IQrcodeService
    {
        public QrcodeService(DefaultDataContext dbcontext) : base(dbcontext)
        {

        }

        public QrcodeViewModel Add(QrcodeViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            Qrcode entity = new Qrcode();
            uow.Set<Qrcode>().Add(entity);
            entity.Id = model.Id;
            entity.Content = model.Content;
            entity.Path = model.Path;
            entity.ExpireAt = model.ExpireAt;
            entity.Purpose = model.Purpose;
            entity.ScanedCount = model.ScanedCount;
            entity.Creator = model.Creator;
            uow.Commit();
            return new QrcodeViewModel(entity);
        }

        public bool Delete(object id)
        {
            Qrcode entity = uow.Set<Qrcode>().Find(id);
            if (entity == null || entity.IsDelete)
                return false;
            entity.IsDelete = true;
            uow.Commit();
            return true;
        }
        public bool Remove(object id)
        {
            Qrcode entity = uow.Set<Qrcode>().Find(id);
            if (entity == null)
                return false;
            uow.Set<Qrcode>().Remove(entity);
            uow.Commit();
            return true;
        }

        public QrcodeViewModel GetById(object id)
        {
            var entity = uow.Set<Qrcode>().Find(id);
            if (entity == null || entity.IsDelete)
                return null;
            return new QrcodeViewModel(entity);
        }

        public IEnumerable<QrcodeViewModel> Query(string key, int offset, int limit, out int count)
        {
            var query = uow.Set<Qrcode>().Where(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Content.Contains(key) || x.Purpose.Contains(key));
            }
            count = query.Count();
            return query.OrderByDescending(x => x.CreateAt).Skip(offset).Take(limit).ToList().Select(x => new QrcodeViewModel(x));
        }

        public void Scan(object id)
        {
            var entity = uow.Set<Qrcode>().Find(id);
            if (entity != null && entity.IsDelete == false)
            {
                entity.ScanedCount++;
                uow.Commit();
            }

        }

        public QrcodeViewModel Update(QrcodeViewModel model)
        {
            var entity = uow.Set<Qrcode>().Find(model.Id);

            if (entity == null)
                return null;

            entity.Content = model.Content;
            entity.Path = model.Path;
            entity.ExpireAt = model.ExpireAt;
            entity.Purpose = model.Purpose;
            entity.ScanedCount = model.ScanedCount;
            entity.Creator = model.Creator;
            uow.Commit();
            return new QrcodeViewModel(entity);
        }
    }
}
