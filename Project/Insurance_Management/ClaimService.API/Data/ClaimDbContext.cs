using Microsoft.EntityFrameworkCore;
using ClaimService.API.Entities;

namespace ClaimService.API.Data
{
    public class ClaimDbContext : DbContext
    {
        public ClaimDbContext(DbContextOptions<ClaimDbContext> options) : base(options) { }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<Query> Queries { get; set; }
    }
}