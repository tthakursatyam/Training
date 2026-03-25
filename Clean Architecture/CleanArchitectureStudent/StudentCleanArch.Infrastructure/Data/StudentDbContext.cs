using Microsoft.EntityFrameworkCore;
using StudentCleanArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCleanArch.Infrastructure.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
    }
}
