using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using UowMVC.Models;
using Autofac;
using UowMVC.Service.Interfaces;

namespace UowMVC.Web.SignalR
{
    public class PushHelper
    {
        /// <summary>
        /// 推送消息到Web客户端
        /// </summary>
        /// <param name="cid">*:所有</param>
        /// <param name="msg"></param>
        public static void PushMessage(string userName)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
            var cids = MessageHub._clients.Where(x => x.Value == userName).Select(x => x.Key).ToList();
            foreach (var cid in cids)
            {
                context.Clients.Client(cid).showMessageCount();
            }

        }
    }
}