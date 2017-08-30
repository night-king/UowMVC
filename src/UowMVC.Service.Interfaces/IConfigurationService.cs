using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface IConfigurationService
    {
        bool Add(ConfigurationViewModel model);

        bool Update(ConfigurationViewModel model);

        ConfigurationViewModel GetById(object id);

        bool Delete(object id);

        IEnumerable<ConfigurationViewModel> GetAll();
    }
}
