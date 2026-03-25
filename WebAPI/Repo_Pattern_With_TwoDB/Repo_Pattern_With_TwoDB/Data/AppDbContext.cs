using Microsoft.EntityFrameworkCore;
using Repo_Pattern_With_TwoDB.Models;

namespace Repo_Pattern_With_TwoDB.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<FileMetadata> Files { get; set; }
    }
}
