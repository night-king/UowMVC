using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface IItemCountService
    {
        /// <summary>
        /// 获取首页统计信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<ItemCountViewModel> GetHome();
    }
}
