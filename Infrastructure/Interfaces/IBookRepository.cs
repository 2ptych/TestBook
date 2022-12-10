using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Infrastructure.Interfaces
{
    public interface IBookRepository
    {
        FileDb GetFileByHashOrNull(string hash);
        void AddFile(FileDb newEntry);
        void Add(BookDb newEntry);
        BookDb GetById(int id);
        List<BookDb> GetLstByIds(List<int> ids);
        void Delete(int id);
        void DropBookRelations(BookDb book);
        List<CategoryDb> GetCategoryLstByIds(List<int> ids);
        List<AuthorDb> GetAuthorLstByIds(List<int> ids);
        public List<BookDb> SearchBooks(
            string searchStr,
            List<int> categoryIds,
            CancellationToken cancellationToken);
    }
}
