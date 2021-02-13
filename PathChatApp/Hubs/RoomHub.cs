using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathChatApp.Hubs
{
    // a hub
    public class RoomHub : Hub
    {
        // get the hub connection id
        public string GetConnectionId() => Context.ConnectionId;
       
    }
}
