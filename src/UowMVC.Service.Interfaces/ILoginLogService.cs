using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface ILoginLogService
    {
        bool Add(LoginLogViewModel model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <param name="result">
        /// 成功
        /// 失败
        /// <returns></returns>
        IEnumerable<LoginLogViewModel> Query(string key, int offset, int limit, out int count, int result = -1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="username"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <param name="result">
        /// 成功
        /// 失败
        /// </param>
        /// <returns></returns>
        IEnumerable<LoginLogViewModel> QueryByUser(string key, string username, int offset, int limit, out int count, int result = -1);

    }
}
