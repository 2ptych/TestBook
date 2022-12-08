using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddBookToUserFavorites(AddBookToFvrtDto requestDto)
        {
            _userRepository.AddBookToUserFavorites(requestDto.Book, requestDto.User);
        }

        public void DeleteBookFromUsersFavorites(DeleteBookFromFvrtDto requestDto)
        {
            _userRepository.DeleteBookFromUserFavorites(requestDto.Book, requestDto.User);
        }

        public List<SearchBooksResponseDto> SearchBooks(
            SearchBooksRequestDto requestDto,
            CancellationToken token)
        {
            var result = new List<SearchBooksResponseDto>();
            if (!token.IsCancellationRequested)
            {
                var books = _userRepository.SearchBooks(
                    requestDto.SearchString,
                    requestDto.CategoryIds,
                    token);

                foreach (var book in books)
                {
                    result.Add(BookDbToSrchBkRespDto(book));
                }
            }
            else token.ThrowIfCancellationRequested();

            return result;
        }

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

            return result;
        }
    }
}
