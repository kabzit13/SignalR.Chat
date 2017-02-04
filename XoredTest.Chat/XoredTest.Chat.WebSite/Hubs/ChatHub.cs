using System;
using Microsoft.AspNet.SignalR;

namespace XoredTest.Chat.WebSite.Hubs
{
    public class ChatHub : Hub
    {
        public void SendToAll(string userName, string msg)
        {
            if(string.IsNullOrWhiteSpace(msg))
                return;

            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = "Anonimus";
            }


            this.Clients.All.broadcastMessage(userName, msg);
        }

        public void SendPrivateTo(string userId, string msg)
        {
            throw new NotImplementedException();
        }
        
    }
}