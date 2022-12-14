using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public FileDb GetFileByHashOrNull(string hash)
        {
            FileDb result = null;

            result = _context.File
                .Where(x => x.Hash == hash)
                .FirstOrDefault();

            return result;
        }

        public BookDb GetById(int id)
        {
            var result = _context.Book
                .Where(x => x.Id == id)
                .Include(x => x.Cover)
                .Include(x => x.BookAuthorJoin)
                .Include(x => x.BookCategoryJoin)
                .First();

            return result;
        }

        public List<BookDb> GetLstByIds(List<int> ids)
        {
            var result = _context.Book
                .Where(x => ids.Contains(x.Id))
                .Include(x => x.Cover)
                .Include(x => x.BookAuthorJoin)
                    .ThenInclude(x => x.Author)
                .Include(x => x.BookCategoryJoin)
                    .ThenInclude(x => x.Category)
                .ToList();

            return result;
        }

        public void DropBookRelations(BookDb book)
        {
            _context.BookAuthorJoin.RemoveRange(book.BookAuthorJoin);
            _context.BookCategoryJoin.RemoveRange(book.BookCategoryJoin);
        }

        public void AddFile(FileDb newEntry)
        {
            _context.File.Add(newEntry);
            _context.SaveChanges();
        }

        public void Add(BookDb newEntry)
        {
            _context.Book.Add(newEntry);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var result = GetById(id);

            _context.Book.Remove(result);
            _context.SaveChanges();
        }

        public List<CategoryDb> GetCategoryLstByIds(List<int> ids)
        {
            List<CategoryDb> cats = _context.Category
                .Where(x => ids.Contains(x.Id))
                .ToList();

            return cats;
        }

        public List<AuthorDb> GetAuthorLstByIds(List<int> ids)
        {
            List<AuthorDb> auth = _context.Author
                .Where(x => ids.Contains(x.Id))
                .ToList();

            return auth;
        }

        public List<BookDb> SearchBooks(
            string searchStr,
            List<int> categoryIds,
            CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                cancellationToken.ThrowIfCancellationRequested();

            var result = new List<BookSearchView>();
            string query = string.Empty;
            if (categoryIds.Count == 0)
            {
                query = string.Format(
                    SqlQueries.BookSearchByTitle,
                    searchStr);

                result = _context.BookSearchView
                    .FromSqlRaw(query)
                    .ToList();
            }
            else
            {
                query = string.Format(
                    SqlQueries.BookSearchByTitleWithCategories,
                    searchStr,
                    string.Join(",", categoryIds));

                result = _context.BookSearchView
                    .FromSqlRaw(query)
                    .ToList();
            }

            if (cancellationToken.IsCancellationRequested)
                cancellationToken.ThrowIfCancellationRequested();

            return GetLstByIds(
                result.Select(x => x.BookId).ToList());
        }
    }
}
