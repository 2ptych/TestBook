using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public void AddBookToUserFavorites(BookDb book, UserDb user)
        {
            _context.UserFavoritesJoin.Add(new UserFavoritesJoinDb
            {
                User = user,
                Book = book
            });
            _context.SaveChanges();
        }

        public void DeleteBookFromUserFavorites(BookDb book, UserDb user)
        {
            var bookInFavorites =
                _context.UserFavoritesJoin
                //.Include(x => x.Book)
                //.Include(x => x.User)
                .Where(x =>
                    (x.UserId == user.Id) &&
                    (x.BookId == book.Id))
                .FirstOrDefault();

            if (bookInFavorites != null)
            {
                _context.UserFavoritesJoin.Remove(bookInFavorites);
                _context.SaveChanges();
            }
        }
    }
}
