using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        LoginResponseDto LoginAsync(LoginRequestDto requestDto);
        LoginResponseDto RefreshToken(RefreshTokenDto requestDto);
    }
}
