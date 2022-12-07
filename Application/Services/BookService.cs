using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IWebHostEnvironment _hosting;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(
            IWebHostEnvironment hosting,
            IBookRepository bookRepository,
            IUnitOfWork unitOfWork)
        {
            _hosting = hosting;
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(AddBookRequestDto requestDto)
        {
            var file = SaveFile(requestDto.File);

            BookDb newEntry = new BookDb
            {
                Description = requestDto.Description,
                PageCount = requestDto.PageCount,
                Title = requestDto.Title,
                Year = requestDto.Year,
                Cover = file,
                BookCategoryJoin = new List<BookCategoryJoin>(),
                BookAuthorJoin = new List<BookAuthorJoinDb>()
            };

            SetCategoriesToBookByIds(newEntry, requestDto.CategoryIds);

            SetAuthorsToBookByIds(newEntry, requestDto.AuthorIds);

            _bookRepository.Add(newEntry);
        }

        public void Delete(DeleteBookRequestDto requestDto)
        {
            _bookRepository.Delete(requestDto.Id);
        }

        public void Update(UpdateBookRequestDto requestDto)
        {
            var entry = _bookRepository.GetById(requestDto.Id);

            _unitOfWork.OpenTransaction();

            entry.Description = requestDto.Description;
            entry.PageCount = requestDto.PageCount;
            entry.Title = requestDto.Title;
            entry.Year = requestDto.Year;

            _bookRepository.DropBookRelations(entry);

            if (requestDto.File != null)
                entry.Cover = SaveFile(requestDto.File);

            SetCategoriesToBookByIds(entry, requestDto.CategoryIds);

            SetAuthorsToBookByIds(entry, requestDto.AuthorIds);

            _unitOfWork.Commit();
        }

        #region Helpers

        private void SetCategoriesToBookByIds(BookDb book, List<int> ids)
        {
            List<CategoryDb> categories =
                _bookRepository.GetCategoryLstByIds(ids);

            foreach (var cat in categories)
            {
                book.BookCategoryJoin.Add(new BookCategoryJoin
                {
                    Book = book,
                    Category = cat
                });
            }
        }

        private void SetAuthorsToBookByIds(BookDb book, List<int> ids)
        {
            List<AuthorDb> authors =
                _bookRepository.GetAuthorLstByIds(ids);

            foreach (var auth in authors)
            {
                book.BookAuthorJoin.Add(new BookAuthorJoinDb
                {
                    Book = book,
                    Author = auth
                });
            }
        }


        private FileDb SaveFile(IFormFile file)
        {
            string savePath = Path.Combine(_hosting.ContentRootPath, "wwwroot", "uploads");

            var stream = new MemoryStream();
            file.CopyTo(stream);

            FileDb result = new FileDb
            {
                FileName = file.FileName,
                Hash = ComputeHash(stream)
            };

            var fullPath = Path.Combine(savePath, result.Hash);

            var existedFile = _bookRepository.GetFileByHashOrNull(result.Hash);

            if (existedFile == null)
            {
                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);

                using (var fStream = new FileStream(
                    fullPath, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fStream);
                }

                _bookRepository.AddFile(result);

                return result;
            }

            return existedFile;
        }

        private string ComputeHash(Stream fStream)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(fStream);
                return BitConverter.ToString(hash)
                    .Replace("-", "")
                    .ToLower();
            }
        }
        #endregion
    }
}
