using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]//[Authorize("admin")]
        [Route("api/book/add")]
        public object AddBook(
            [FromForm] AddBookRequestDto requestDto)
        {
            try
            {
                if ((HttpContext.Request.Form.Files != null) &&
                    (HttpContext.Request.Form.Files.Count == 1))
                {
                    requestDto.File = Request.Form.Files[0];

                    if (CheckAllowedFileFormat(requestDto.File))
                    {
                        _bookService.Add(requestDto);
                        return Ok();
                    }
                }
                
                return BadRequest(
                    "В качестве обложки необходимо " +
                    "приложить 1 файл в формате jpg");
            }
            catch (Exception)
            {
                return BadRequest("Не удалось добавить книгу");
            }
        }

        [HttpGet]
        [Authorize("admin")]
        [Route("api/book/delete/{requestDto.Id}")]
        public object DeleteBook([FromRoute] DeleteBookRequestDto requestDto)
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
        [Authorize("admin")]
        [Route("api/book/update")]
        public object UpdateBook([FromForm] UpdateBookRequestDto requestDto)
        {
            try
            {
                if ((HttpContext.Request.Form.Files != null) &&
                    (HttpContext.Request.Form.Files.Count == 1) &&
                    (CheckAllowedFileFormat(Request.Form.Files[0])))
                {
                    requestDto.File = Request.Form.Files[0];
                }

                _bookService.Update(requestDto);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Не удалось удалить книгу");
            }
        }

        #region Helpers
        private bool CheckAllowedFileFormat(IFormFile file)
        {
            bool result = false;
            List<string> allowedExtensions = new List<string>
            {
                ".jpg"
            };

            string extension = Path.GetExtension(file.FileName).ToLower();

            foreach (var ext in allowedExtensions)
            {
                if (ext == extension)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
        #endregion
    }
}
