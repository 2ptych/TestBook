using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserService
    {
        void AddBookToUserFavorites(AddBookToFvrtDto requestDto);
        void DeleteBookFromUsersFavorites(DeleteBookFromFvrtDto requestDto);
    }
}
