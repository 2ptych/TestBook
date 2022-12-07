﻿using Application.DTOs;
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

        public BookService(IWebHostEnvironment hosting,
            IBookRepository bookRepository)
        {
            _hosting = hosting;
            _bookRepository = bookRepository;
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

            List<CategoryDb> categories =
                _bookRepository.GetCategoryLstByIds(requestDto.CategoryIds);

            foreach (var cat in categories)
            {
                newEntry.BookCategoryJoin.Add(new BookCategoryJoin
                {
                    Book = newEntry,
                    Category = cat
                });
            }

            List<AuthorDb> authors =
                _bookRepository.GetAuthorLstByIds(requestDto.AuthorIds);

            foreach (var auth in authors)
            {
                newEntry.BookAuthorJoin.Add(new BookAuthorJoinDb
                {
                    Book = newEntry,
                    Author = auth
                });
            }

            _bookRepository.Add(newEntry);
        }

        public void Delete(DeleteBookRequestDto requestDto)
        {
            _bookRepository.Delete(requestDto.Id);
        }

        #region Helpers
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