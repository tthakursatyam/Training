using Microsoft.EntityFrameworkCore;
using MVC_With_EF.Models;
namespace MVC_With_EF.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
