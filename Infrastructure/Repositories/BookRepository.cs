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
            var result = _context.Book
                .Where(x => x.Id == id)
                .Include(x => x.Cover)
                .First();

            _context.Book.Remove(result);
            _context.SaveChanges();
        }
    }
}
