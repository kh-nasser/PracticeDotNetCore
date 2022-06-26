using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SignalRTest_chat_dotNet_simple.Hubs
{
    public class chatHub : Hub
    {
        public static ConcurrentDictionary<string, List<string>> ConnectedUsers = new ConcurrentDictionary<string, List<string>>();

        public override Task OnConnected()
        {
            Debug.WriteLine("OnConnected:" + Context.ConnectionId);

            Trace.TraceInformation("MapHub started. ID: {0}", Context.ConnectionId);

            var userName = "testUserName1"; // or get it from Context.User.Identity.Name;

            // Try to get a List of existing user connections from the cache
            List<string> existingUserConnectionIds;
            ConnectedUsers.TryGetValue(userName, out existingUserConnectionIds);

            // happens on the very first connection from the user
            if (existingUserConnectionIds == null)
            {
                existingUserConnectionIds = new List<string>();
            }

            // First add to a List of existing user connections (i.e. multiple web browser tabs)
            existingUserConnectionIds.Add(Context.ConnectionId);


            // Add to the global dictionary of connected users
            ConnectedUsers.TryAdd(userName, existingUserConnectionIds);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Debug.WriteLine("OnDisconnected:" + Context.ConnectionId);

            var userName = Context.User.Identity.Name;

            List<string> existingUserConnectionIds;
            ConnectedUsers.TryGetValue(userName, out existingUserConnectionIds);
            if (existingUserConnectionIds != null)
            {
                // remove the connection id from the List 
                existingUserConnectionIds.Remove(Context.ConnectionId);

                // If there are no connection ids in the List, delete the user from the global cache (ConnectedUsers).
                if (existingUserConnectionIds.Count == 0)
                {
                    // if there are no connections for the user,
                    // just delete the userName key from the ConnectedUsers concurent dictionary
                    List<string> garbage; // to be collected by the Garbage Collector
                    ConnectedUsers.TryRemove(userName, out garbage);
                }
            }
            return base.OnDisconnected(stopCalled);
        }

        public void send(string name, string message)
        {
            Debug.WriteLine("send:" + Context.ConnectionId);

            Clients.All.addNewMessage(name, message);
        }
    }
}