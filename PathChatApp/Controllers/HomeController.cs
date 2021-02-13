using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PathChatApp.Data;

using PathChatApp.Extensions;
using PathChatApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PathChatApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IDistributedCache _cache;

        /// <summary>
        ///  Inject an instance of the database and the caching server
        /// </summary>
        /// <param name="context">database instance</param>
        /// <param name="cache">redis cashing server instance</param>
        public HomeController(ApplicationDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }
        /// <summary>
        /// App Default page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Retrive Room data by Id
        /// </summary>
        /// <param name="id">Room Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Room(Guid id)
        {
            // a variable to hold room data
            Room retrunedData = new Room();

            // create a recored id to store or get the cached data
            string recordId = "PathCashApp_" + DateTime.Now.ToString("yyyymmdd_hhmm");

            // get data from cache
            var room = await _cache.GetRecoredAsync<Room>(recordId);

            // if there no cached data
            if (room is null)
            {
                // get the data from the database
                retrunedData = await _context.Rooms.Include(x => x.Messages).FirstOrDefaultAsync(x => x.Id == id);

                // cache the retrived data
                await _cache.SetRecordAsync(recordId, retrunedData);

            }
            // otherwise
            else
            {
                // if the room id equals the cached room id
                if (room.Id == id)
                {
                    // then get the cached data
                    retrunedData = room;
                }
                // otherwise
                else
                {
                    // get room data from the database
                    retrunedData = await _context.Rooms.Include(x => x.Messages).FirstOrDefaultAsync(x => x.Id == id);
                }
            }

            return View(retrunedData);

        }


        /// <summary>
        /// create a room
        /// </summary>
        /// <param name="name">room name</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateChatRoom(string name)
        {
            //create a new room
            var room = new Room { RoomName = name };

            // add it to the database
            _context.Rooms.Add(room);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }


        //[HttpPost]
        //public async Task<IActionResult> SendMessage(string roomId, string message)
        //{
        //    var newMessage = new Message
        //    {
        //        RoomId = Guid.Parse(roomId) ,
        //        MessageText = message,
        //        SenderName = "Default",
        //        CreatedTime = DateTime.Now
        //    };

        //    _context.Messages.Add(newMessage);

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Room", new { id = roomId});

        //}


    }
}
