using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface IQrcodeService
    {
        QrcodeViewModel Add(QrcodeViewModel model);

        QrcodeViewModel Update(QrcodeViewModel model);

        QrcodeViewModel GetById(object id);

        void Scan(object id);

        bool Delete(object id);

        bool Remove(object id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<QrcodeViewModel> Query(string key, int offset, int limit, out int count);
    }
}
