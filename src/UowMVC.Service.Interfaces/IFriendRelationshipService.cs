using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface IFriendRelationshipService
    {
        bool Add(FriendRelationshipViewModel model);

        bool Update(FriendRelationshipViewModel model);

        FriendRelationshipViewModel GetById(object id);

        bool Delete(object id);

        IEnumerable<FriendRelationshipViewModel> Query(string key, int offset, int limit, out int count, string ownerId = "");
    }
}
