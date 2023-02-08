using backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class XimDbContext : IdentityDbContext
    {
        public XimDbContext(DbContextOptions<XimDbContext> options)
            : base(options)
        { }

        public DbSet<Room> Rooms { get; set; }
    }
}