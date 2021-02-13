using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathChatApp.Hubs
{
    public class JoinRoomRequestDto
    {
        public string ConnectionId { get; set; }

        public string RoomName { get; set; }
    }
}
