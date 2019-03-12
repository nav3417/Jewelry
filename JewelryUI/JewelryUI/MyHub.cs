using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace JewelryUI
{
    [HubName("MyNewHub")]
    public class MyHub : Hub
    {

        static int count = 0;
        //static string name = "Name";
        public void Recordhit()
        {
            count++;
            //  Clients.All.hello();
            Clients.All.onrecordhit(count);
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            count--;
            Clients.All.onrecordhit(count);
            return base.OnDisconnected(stopCalled);
        }
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
    }
}