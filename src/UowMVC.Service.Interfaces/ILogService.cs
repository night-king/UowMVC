using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface ILogService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <param name="level">
        /// INFO   信息
        /// WARN   警告
        /// ERROR  错误
        /// FATAL  致命错误
        /// </param>
        /// <returns></returns>
        IEnumerable<LogViewModel> Query(string key, int offset, int limit, out int count, string level = null);

        void Report(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void Resolve(string id);
        LogViewModel GetById(string id);

    }
}
