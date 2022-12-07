using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IBookService
    {
        void Add(AddBookRequestDto requestDto);
        void Update(UpdateBookRequestDto requestDto);
        void Delete(DeleteBookRequestDto requestDto);
    }
}
