using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface IDictService
    {
        bool Add(DictViewModel model);

        bool Update(DictViewModel model);

        DictViewModel GetById(object id);

        IEnumerable<DictViewModel> GetByIndexNo(string no);
        DictViewModel GetByKey(string key);
        bool Delete(object id);
      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        IEnumerable<DictViewModel> Query(string key, int offset, int limit, out int count, string index = "");

    }
}
