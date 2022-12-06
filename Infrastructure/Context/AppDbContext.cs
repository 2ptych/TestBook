using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDb>()
                .HasIndex(x => x.UserName)
                .IsUnique();
        }

        public DbSet<RoleDb> Role { get; set; }
        public DbSet<UserDb> Users { get; set; }
        public DbSet<RefreshTokenDb> RefreshTokens { get; set; }
    }
}
