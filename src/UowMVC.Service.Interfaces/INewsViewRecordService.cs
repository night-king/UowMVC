using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface INewsViewRecordService
    {
        bool Add(NewsViewRecordViewModel model);

        bool Update(NewsViewRecordViewModel model);

        NewsViewRecordViewModel GetById(object id);

        bool Delete(object id);

        NewsViewRecordViewModel QueryByStudent(string newsId, string studentId);
        int QueryDistinctCountByStudent(string studentId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        IEnumerable<NewsViewRecordViewModel> Query(string key, int offset, int limit, out int count, string studentId = "");

    }
}
