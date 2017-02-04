using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using XoredTest.Chat.Domain;

namespace XoredTest.Chat.WebSite.Hubs
{
    /// <summary>
    /// Hub for proccessing chat
    /// </summary>
    public class ChatHub : Hub
    {
        private static List<User> connectedUsers = new List<User>();

        /// <summary>
        /// Sends messages to all user in public room
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="msg"></param>
        public void SendToAll(string userName, string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
                return;
            
            this.Clients.All.broadcastMessage(userName, msg);
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="name"></param>
        public void Connect(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = this.AddAnonymusUser();
                this.Clients.Caller.setNewName(name);
            }
            else
            {
                connectedUsers.Add(new User(this.Context.ConnectionId, name, UserType.General));
            }
            this.Clients.AllExcept(this.Context.ConnectionId).notifyAboutNewUser(name);
        }

        /// <summary>
        /// Add anonymus user
        /// </summary>
        /// <returns></returns>
        private string AddAnonymusUser()
        {
            var anonCount = connectedUsers.Count(t => t.UserType == UserType.Anonymous);

            var userName = $"Anonimus";
            connectedUsers.Add(new User(this.Context.ConnectionId, userName, UserType.Anonymous ));
            return userName;
        }

        /// <summary>
        /// Sends message to user by id
        /// </summary>
        /// <param name="userId">Reciever id</param>
        /// <param name="msg">Message text</param>
        public void SendPrivateTo(string userId, string msg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// On user disconnect
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            var user = connectedUsers.FirstOrDefault(t => t.Id == this.Context.ConnectionId);

            if (user != null)
            {
                connectedUsers.Remove(user);

                this.Clients.All.notifyAboutLeftUser(user.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}