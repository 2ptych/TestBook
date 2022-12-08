using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Application.Interfaces
{
    public interface IUserService
    {
        void AddBookToUserFavorites(AddBookToFvrtDto requestDto);
        void DeleteBookFromUsersFavorites(DeleteBookFromFvrtDto requestDto);
        SearchBooksResponseDto SearchBooks(
            SearchBooksRequestDto requestDto,
            CancellationToken token);
    }
}
