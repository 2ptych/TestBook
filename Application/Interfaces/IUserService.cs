using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Application.Interfaces
{
    public interface IUserService
    {
        void AddBookToUserFavorites(UserFavoritesHandleDto requestDto);
        void DeleteBookFromUsersFavorites(UserFavoritesHandleDto requestDto);
        List<SearchBooksResponseDto> SearchBooks(
            SearchBooksRequestDto requestDto,
            CancellationToken token);
    }
}
