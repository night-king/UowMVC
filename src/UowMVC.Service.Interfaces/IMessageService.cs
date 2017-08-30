using UowMVC.Domain;
using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface IMessageService
    {
        bool Add(MessageViewModel model);

        bool Update(MessageViewModel model);

        MessageViewModel GetById(object id,bool isReaded=false);

        bool Delete(object id);
        bool Remove(object id);
        bool Read(object id);
        bool UnRead(object id);

        IEnumerable<MessageViewModel> Query(string key, int offset, int limit, out int count, string ownerId = "");
        IEnumerable<MessageViewModel> QueryAccept(string key, int offset, int limit, out int count, MessageStatusEnum status, MessageTypeEnum type, string ownerId = "");
        IEnumerable<MessageViewModel> QuerySend(string key, int offset, int limit, out int count, MessageStatusEnum status, MessageTypeEnum type, string ownerId = "");
        IEnumerable<MessageViewModel> QueryRecycle(string key, int offset, int limit, out int count, string ownerId = "");

        IEnumerable<MessageViewModel> QueryReceiveNew(string userName, out int count);
        int QueryAcceptCount(string userId, MessageStatusEnum status, MessageTypeEnum type);
        int QuerySendCount(string userId, MessageStatusEnum status, MessageTypeEnum type);

    }
}
