using backend.Models;
using Microsoft.AspNetCore.Identity;
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
        public DbSet<RoomAttendee> RoomAttendees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RoomAttendee>()
                .HasKey(ra => new { ra.AppUserId, ra.RoomId });

            builder.Entity<RoomAttendee>()
                .HasOne(ra => ra.AppUser)
                .WithMany(au => au.Rooms)
                .HasForeignKey(ra => ra.AppUserId);

            builder.Entity<RoomAttendee>()
                .HasOne(ra => ra.Room)
                .WithMany(r => r.Attendees)
                .HasForeignKey(ra => ra.RoomId);
        }
    }
}