using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IBookRepository _bookRepository;

        public UserRepository(AppDbContext context,
            IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
        }

        public void AddBookToUserFavorites(BookDb book, UserDb user)
        {
            var existed = _context.UserFavoritesJoin.AsNoTracking()
                //.Include(x => x.Book)
                //.Include(x => x.User)
                .Where(x =>
                    (x.UserId == user.Id) &&
                    (x.BookId == book.Id))
                .FirstOrDefault();

            if (existed == null)
            {
                _context.UserFavoritesJoin.Add(new UserFavoritesJoinDb
                {
                    User = user,
                    Book = book
                });
                _context.SaveChanges();
            }
        }

        public void DeleteBookFromUserFavorites(BookDb book, UserDb user)
        {
            var bookInFavorites =
                _context.UserFavoritesJoin.AsNoTracking()
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
