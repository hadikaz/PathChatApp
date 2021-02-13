using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathChatApp.Models
{
    [Serializable]
    public class Room
    {
        public Room()
        {
            Messages = new List<Message>();
        }

        public Guid Id { get; set; }

        public string RoomName { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
