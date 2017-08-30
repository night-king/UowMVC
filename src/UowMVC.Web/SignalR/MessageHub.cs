using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace UowMVC.Web.SignalR
{
    /// <summary>
    /// 消息推送
    /// </summary>
    [HubName("MessageHub")]
    public class MessageHub : Hub
    {
        /// <summary>
        /// 客户端信息
        /// cid,UserId
        /// </summary>
        public static Dictionary<string, string> _clients = new Dictionary<string, string>();
        public void notifyConnected(string userIdentity, string conId)
        {
            lock (_clients)
            {
                if (!_clients.Any(x => x.Key == conId))
                {
                    _clients.Add(conId, userIdentity);
                }
            }

        }
        public override Task OnConnected()
        {
            // Add your own code here.
            // For example: in a chat application, record the association between
            // the current connection ID and user name, and mark the user as online.
            // After the code in this method completes, the client is informed that
            // the connection is established; for example, in a JavaScript client,
            // the start().done callback is executed.
            string cid = Context.ConnectionId;
            if (Context.Request.User != null)
            {
                var user = Context.Request.User.Identity.Name;
                lock (_clients)
                {
                    if (!_clients.Any(x => x.Key == cid))
                    {
                        _clients.Add(cid, user);
                    }
                }
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            // Add your own code here.
            // For example: in a chat application, mark the user as offline, 
            // delete the association between the current connection id and user name.
            string cid = Context.ConnectionId;
            lock (_clients)
            {
                if (_clients.Any(x => x.Key == cid))
                {
                    _clients.Remove(cid);
                }
            }
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            // Add your own code here.
            // For example: in a chat application, you might have marked the
            // user as offline after a period of inactivity; in that case 
            // mark the user as online again.
            string cid = Context.ConnectionId;
            lock (_clients)
            {
                if (!_clients.Any(x => x.Key == cid))
                {
                    if (Context.Request.User != null)
                    {
                        var user = Context.Request.User.Identity.Name;
                        _clients.Add(cid, user);
                    }
                }
            }
            return base.OnReconnected();
        }
    }
}