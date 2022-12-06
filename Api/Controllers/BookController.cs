using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [Route("api/book/add")]
        public async Task<object> AddBook(AddBookRequestDto requestDto)
        {
            try
            {
                if ((Request.Form.Files != null) &&
                    (Request.Form.Files.Count == 1))
                {
                    requestDto.File = Request.Form.Files[0];
                    _bookService.Add(requestDto);
                    return Ok();
                }
                else 
                    return BadRequest("В качестве обложки необходимо приложить 1 файл");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/book/delete/{requestDto.Id}")]
        public async Task<object> DeleteBook(DeleteBookRequestDto requestDto)
        {
            try
            {
                _bookService.Delete(requestDto);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Не удалось удалить книгу");
            }
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
