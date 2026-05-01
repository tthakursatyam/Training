using Microsoft.EntityFrameworkCore;
using PolicyService.API.Entities;

namespace PolicyService.API.Data
{
    public class PolicyDbContext : DbContext
    {
        public PolicyDbContext(DbContextOptions<PolicyDbContext> options) : base(options)
        {
        }

        public DbSet<Policy> Policies { get; set; }
        public DbSet<CustomerPolicy> CustomerPolicies { get; set; }
    }
}