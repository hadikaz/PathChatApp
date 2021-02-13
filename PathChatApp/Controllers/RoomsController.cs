using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PathChatApp.Data;
using PathChatApp.Hubs;
using PathChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathChatApp.Controllers
{
    [Route("[controller]")]
    public class RoomsController : Controller
    {
        private readonly IHubContext<RoomHub> _room;

        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Inject an instance of the database and a hub
        /// </summary>
        /// <param name="room"></param>
        /// <param name="context"></param>
        public RoomsController(IHubContext<RoomHub> room, ApplicationDbContext context)
        {
            _room = room;

            _context = context;
        }

        /// <summary>
        /// Join a chat room 
        /// </summary>
        /// <param name="req">req object contains the hub connection id, and the room name</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  JoinRoom([FromBody]JoinRoomRequestDto req)
        {
            // join a room
            await _room.Groups.AddToGroupAsync(req.ConnectionId, req.RoomName);

            return Ok();
        }

       /// <summary>
       /// Send a message
       /// </summary>
       /// <param name="roomId">room id</param>
       /// <param name="message">message text</param>
       /// <param name="roomName">room name</param>
       /// <param name="senderName">the sender</param>
       /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(string roomId, string message, string roomName, string senderName)
        {
            // create a message
            var newMessage = new Message
            {
                RoomId = Guid.Parse(roomId),
                MessageText = message,
                SenderName = senderName,
                CreatedTime = DateTime.Now
            };
            // save it in database
            _context.Messages.Add(newMessage);

            await _context.SaveChangesAsync();

            // send the message to all active clients
            await _room.Clients.Group(roomName).SendAsync("RecieveMessage", new {
               
                MessageText = newMessage.MessageText,
                SenderName = newMessage.SenderName,
                CreatedTime = newMessage.CreatedTime.ToString("dd/MM/yy hh:mm:ss")
            });
            return Ok();
        }
    }
}
