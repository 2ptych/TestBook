using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IBookService
    {
        void Add(AddBookRequestDto requestDto);
        void Delete(DeleteBookRequestDto requestDto);
    }
}
