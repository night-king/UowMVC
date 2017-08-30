using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface INewsService
    {
        bool Add(NewsViewModel model);

        bool Update(NewsViewModel model);

        NewsViewModel GetById(object id);

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
        IEnumerable<NewsViewModel> Query(string key, int offset, int limit, out int count, int year = 0, string categoryId = "");
        int QueryCount(int year);
    }
}
