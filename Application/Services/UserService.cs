using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookService _bookService;

        public UserService(IUserRepository userRepository,
            IBookService bookService)
        {
            _userRepository = userRepository;
            _bookService = bookService;
        }

        public void AddBookToUserFavorites(UserFavoritesHandleDto requestDto)
        {
            _userRepository.AddBookToUserFavorites(requestDto.Book, requestDto.User);
        }

        public void DeleteBookFromUsersFavorites(UserFavoritesHandleDto requestDto)
        {
            _userRepository.DeleteBookFromUserFavorites(requestDto.Book, requestDto.User);
        }

        public List<SearchBooksResponseDto> SearchBooks(
            SearchBooksRequestDto requestDto,
            CancellationToken token)
        {
            var result = _bookService.SearchBooks(
                requestDto, token);

            return result;
        }
    }
}
