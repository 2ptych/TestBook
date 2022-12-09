using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("api/login")]
        public object Login(LoginRequestDto requestDto)
        {
            try
            {
                var result = _authenticationService.Login(requestDto);
                return result;
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
            catch
            {
                return BadRequest("Не удалось авторизоваться");
            }
            
        }

        [HttpPost]
        [Route("api/refresh")]
        public object RefreshToken([FromBody] RefreshTokenDto requestDto)
        {
            try
            {
                var result = _authenticationService.RefreshToken(requestDto);
                return result;
            }
            catch (UnauthorizedAccessException ex)
            {
                return new UnauthorizedResult();
            }
            catch
            {
                return BadRequest("Не удалось авторизоваться");
            }
        }
    }
}
