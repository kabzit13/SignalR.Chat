using System;
using System.Dynamic;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using XoredTest.Chat.WebSite.Hubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XoredTest.Chat.Tests
{
    [TestClass]
    public class HubTests
    {
        [TestMethod]
        public void BroadcastMessageIsCalled()
        {
            bool sendCalled = false;
            var hub = new ChatHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            hub.Clients = mockClients.Object;
            dynamic all = new ExpandoObject();
            all.broadcastMessage = new Action<string, string>((name, message) => {
                sendCalled = true;
            });
            mockClients.Setup(m => m.All).Returns((ExpandoObject)all);
            hub.SendToAll("New user", "Hello world");
            Assert.IsTrue(sendCalled);
        }
    }
}
