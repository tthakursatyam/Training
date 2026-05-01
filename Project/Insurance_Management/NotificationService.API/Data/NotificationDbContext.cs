using Microsoft.EntityFrameworkCore;
using NotificationService.API.Entities;

namespace NotificationService.API.Data
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }

        public DbSet<Notification> Notifications { get; set; }
    }
}
