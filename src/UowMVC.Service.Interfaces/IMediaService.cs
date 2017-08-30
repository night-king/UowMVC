using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface IMediaService
    {
        MediaViewModel Add(MediaViewModel model);

        bool Delete(object id);

        MediaViewModel GetById(object id);

        IEnumerable<MediaViewModel> Query(string key, int offset, int limit, out int count);

    }
}
