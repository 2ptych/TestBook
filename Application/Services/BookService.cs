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
using System.Threading;

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

        public List<SearchBooksResponseDto> SearchBooks(
            SearchBooksRequestDto requestDto,
            CancellationToken token)
        {

            var result = new List<SearchBooksResponseDto>();

            if (token.IsCancellationRequested)
                token.ThrowIfCancellationRequested();

            var books = _bookRepository.SearchBooks(
                    requestDto.SearchString,
                    requestDto.CategoryIds,
                    token);

            foreach (var book in books)
            {
                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();

                result.Add(BookDbToSrchBkRespDto(book));


            }

            return result;
        }

        /*public string GetBookCoverLink(BookDb book)
        {

        }*/

        #region Helpers

        private SearchBooksResponseDto BookDbToSrchBkRespDto(BookDb book)
        {
            var result = new SearchBooksResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                Authors = new List<SBObject>(),
                Categories = new List<SBObject>()
            };

            foreach (var auth in book.BookAuthorJoin)
            {
                result.Authors.Add(new SBObject
                {
                    Id = auth.Author.Id,
                    Name = auth.Author.FullName
                });
            }

            foreach (var cat in book.BookCategoryJoin)
            {
                result.Categories.Add(new SBObject
                {
                    Id = cat.Category.Id,
                    Name = cat.Category.Title
                });
            }

            if (book.Cover != null)
                result.Cover =
                    string.Join(
                        "/",
                        GetPicturePath(),
                        book.Cover.Hash) +
                        Path.GetExtension(book.Cover.FileName);

            return result;
        }

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

        public string GetPicturePath()
        {
            return "http://localhost:5555/images";
                //_hosting.ContentRootPath, "wwwroot", "uploads");
        }

        public string GetUploadPath()
        {
            return Path.Combine(
                _hosting.ContentRootPath, "wwwroot", "uploads");
        }

        private FileDb SaveFile(IFormFile file)
        {
            string savePath = GetUploadPath();

            var stream = new MemoryStream();
            file.CopyTo(stream);

            FileDb result = new FileDb
            {
                FileName = file.FileName,
                Hash = ComputeHash(stream)
            };

            var fullPath = Path.Combine(
                savePath,
                result.Hash + Path.GetExtension(file.FileName));

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
            var result = "";
            fStream.Position = 0;
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(fStream);
                result = BitConverter.ToString(hash)
                    .Replace("-", "")
                    .ToLower();
            }
            return result;
        }
        #endregion
    }
}
