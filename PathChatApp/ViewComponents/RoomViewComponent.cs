using Microsoft.AspNetCore.Mvc;
using PathChatApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathChatApp.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public RoomViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var rooms = _context.Rooms.ToList();
            return View(rooms);
        }
    }
}
