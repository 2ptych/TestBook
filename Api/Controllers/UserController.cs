using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("api/add-to-favorites")]
        public async Task<object> AddBookToFvrt(AddBookToFvrtDto requestDto)
        {
            try
            {
                _userService.AddBookToUserFavorites(requestDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Не удалось добавить книгу в избранное");
            }
        }

        [HttpPost]
        [Route("api/remove-from-favorites")]
        public async Task<object> DeleteBookFromFvrt(DeleteBookFromFvrtDto requestDto)
        {
            try
            {
                _userService.DeleteBookFromUsersFavorites(requestDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Не удалось удалить книгу из избранного");
            }
        }

        [HttpPost]
        [Route("api/search-books")]
        public object SearchBooks(
            [FromBody]
            SearchBooksRequestDto requestDto,
            CancellationToken cancellationToken)
        {
            try
            {
                var result =
                    _userService.SearchBooks(requestDto, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Не удалось произвести поиск");
            }
        }
    }
}
