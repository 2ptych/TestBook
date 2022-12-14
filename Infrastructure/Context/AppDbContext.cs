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
            modelBuilder.Entity<BookSearchView>().HasNoKey();

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

            modelBuilder.Entity<UserFavoritesJoinDb>()
                .HasKey(x => new { x.BookId, x.UserId });
            modelBuilder.Entity<UserFavoritesJoinDb>()
                .HasOne(x => x.Book)
                .WithMany(m => m.UserFavoritesJoin)
                .HasForeignKey(x => x.BookId);
            modelBuilder.Entity<UserFavoritesJoinDb>()
                .HasOne(x => x.User)
                .WithMany(m => m.UserFavoritesJoin)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserDb>()
                .HasIndex(x => x.UserName)
                .IsUnique();
        }

        public DbSet<BookSearchView> BookSearchView { get; set; }
        public DbSet<RoleDb> Role { get; set; }
        public DbSet<UserDb> Users { get; set; }
        public DbSet<BookDb> Book { get; set; }
        public DbSet<RefreshTokenDb> RefreshTokens { get; set; }
        public DbSet<BookCategoryJoin> BookCategoryJoin { get; set; }
        public DbSet<BookAuthorJoinDb> BookAuthorJoin { get; set; }
        public DbSet<CategoryDb> Category { get; set; }
        public DbSet<FileDb> File { get; set; }
        public DbSet<AuthorDb> Author { get; set; }
        public DbSet<UserFavoritesJoinDb> UserFavoritesJoin { get; set; }
    }
}
