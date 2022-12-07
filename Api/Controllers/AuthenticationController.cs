using Application.DTOs;
using Application.Interfaces;
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
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<object> Login(LoginRequestDto requestDto)
        {
            try
            {
                var result = _authenticationService.LoginAsync(requestDto);
                return result;
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
        }

        [HttpPost]
        [Route("api/refresh")]
        public async Task<object> RefreshToken([FromBody] RefreshTokenDto requestDto)
        {
            try
            {
                var result = _authenticationService.RefreshToken(requestDto);
                return result;
            }
            catch (UnauthorizedAccessException ex)
            {
                return new ForbidResult();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
