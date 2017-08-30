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
    public class ConfigurationService : ServiceBase, IConfigurationService
    {
        public ConfigurationService(DefaultDataContext dbcontext):base(dbcontext)
        {
        }

        public bool Add(ConfigurationViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            Configuration entity = new Configuration();
            uow.Set<Configuration>().Add(entity);
            entity.Id = model.Id;
            entity.Key = model.Key;
            entity.Value = model.Value;
            entity.No = model.No;
            entity.Type = (ConfigurationTypeEnum)model.Type;
            uow.Commit();
            return true;
        }

        public bool Delete(object id)
        {
            Configuration entity = uow.Set<Configuration>().Find(id);
            if (entity == null)
                return false;
            uow.Set<Configuration>().Remove(entity);
            uow.Commit();
            return true;
        }

        public IEnumerable<ConfigurationViewModel> GetAll()
        {
            return uow.Set<Configuration>().OrderBy(x => x.No).ToList().Select(x => new ConfigurationViewModel(x));

        }

        public ConfigurationViewModel GetById(object id)
        {
            var entity = uow.Set<Configuration>().Find(id);
            if (entity == null)
                return null;
            return new ConfigurationViewModel(entity);
        }

        public bool Update(ConfigurationViewModel model)
        {
            var entity = uow.Set<Configuration>().Find(model.Id);

            if (entity == null)
                return false;

            entity.Key = model.Key;
            entity.Value = model.Value;
            entity.No = model.No;
            entity.Type = (ConfigurationTypeEnum)model.Type;
            uow.Commit();
            return true;
        }
    }
}
