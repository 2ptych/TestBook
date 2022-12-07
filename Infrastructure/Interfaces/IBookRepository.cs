using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces
{
    public interface IBookRepository
    {
        FileDb GetFileByHashOrNull(string hash);
        void AddFile(FileDb newEntry);
        void Add(BookDb newEntry);
        void Delete(int id);
        List<CategoryDb> GetCategoryLstByIds(List<int> ids);
        List<AuthorDb> GetAuthorLstByIds(List<int> ids);
    }
}
