using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
