using AuthService.API.Data;
using AuthService.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService.API.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAsync(AuthDbContext context)
        {
            if (!await context.Roles.AnyAsync())
            {
                var roles = new List<Role>
                {
                    new Role { Name = "Admin" },
                    new Role { Name = "Agent" },
                    new Role { Name = "Customer" },
                    new Role { Name = "ClaimAdjuster" }
                };

                await context.Roles.AddRangeAsync(roles);
                await context.SaveChangesAsync();
            }
        }
        public static async Task SeedAdminAsync(AuthDbContext context)
        {
            if (!await context.Users.AnyAsync(x => x.Email == "admin@insurance.com"))
            {
                var role = await context.Roles.FirstAsync(x => x.Name == "Admin");

                var admin = new User
                {
                    Id = Guid.NewGuid(),
                    Name = "System Admin",
                    Email = "admin@insurance.com",
                    RoleId = role.Id,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                };

                var hasher = new PasswordHasher<User>();
                admin.PasswordHash = hasher.HashPassword(admin, "Admin@123");

                await context.Users.AddAsync(admin);
                await context.SaveChangesAsync();
            }
        }
    }

}