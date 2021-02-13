using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PathChatApp.Models
{
    
    public class Message
    {
        public Guid Id { get; set; }

        public string SenderName { get; set; }

        public string MessageText { get; set; }

        public DateTime CreatedTime { get; set; }

        public Guid RoomId { get; set; }
        [JsonIgnore]
        public Room Room { get; set; }
    }
}
