using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PathChatApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PathChatApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}
