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
            // один-ко-многим книги-авторы
            modelBuilder.Entity<BookAuthorJoinDb>()
                .HasKey(x => new { x.AuthorId, x.BookId });
            modelBuilder.Entity<BookAuthorJoinDb>()
                .HasOne(x => x.Author)
                .WithMany(x => x.BookAuthorJoin)
                .HasForeignKey(x => x.AuthorId);
            modelBuilder.Entity<BookAuthorJoinDb>()
                .HasOne(x => x.Book)
                .WithMany(x => x.BookAuthorJoin)
                .HasForeignKey(x => x.BookId);

            // один-ко-многим файлы-книги
            /*modelBuilder.Entity<BookFileJoinDb>()
                .HasKey(x => new { x.BookId, x.FileId });
            modelBuilder.Entity<BookFileJoinDb>()
                .HasOne(x => x.Book)
                .WithMany(x => x.BookFileJoin)
                .HasForeignKey(x => x.BookId);
            modelBuilder.Entity<BookFileJoinDb>()
                .HasOne(x => x.File)
                .WithMany(x => x.BookFileJoin)
                .HasForeignKey(x => x.FileId);*/

            //
            modelBuilder.Entity<BookCategoryJoin>()
                .HasKey(x => new { x.BookId, x.CategoryId });
            modelBuilder.Entity<BookCategoryJoin>()
                .HasOne(x => x.Book)
                .WithMany(x => x.BookCategoryJoin)
                .HasForeignKey(x => x.BookId);
            modelBuilder.Entity<BookCategoryJoin>()
                .HasOne(x => x.Category)
                .WithMany(x => x.BookCategoryJoin)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<UserDb>()
                .HasIndex(x => x.UserName)
                .IsUnique();
        }

        public DbSet<RoleDb> Role { get; set; }
        public DbSet<UserDb> Users { get; set; }
        public DbSet<BookDb> Book { get; set; }
        public DbSet<RefreshTokenDb> RefreshTokens { get; set; }
        //public DbSet<BookFileJoinDb> BookFileJoin { get; set; }
        public DbSet<BookAuthorJoinDb> BookAuthorJoin { get; set; }
        public DbSet<CategoryDb> Category { get; set; }
        public DbSet<FileDb> File { get; set; }

    }
}
