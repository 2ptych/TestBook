using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenIssuerService _tokenIssuerService;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(
            ITokenIssuerService tokenIssuerService,
            IAuthenticationRepository authenticationRepository,
            IUnitOfWork unitOfWork)
        {
            _tokenIssuerService = tokenIssuerService;
            _authenticationRepository = authenticationRepository;
            _unitOfWork = unitOfWork;
        }

        public class TokenOutput
        {
            public string AccessToken { get; set; }
        }

        public LoginResponseDto LoginAsync(LoginRequestDto requestDto)
        {
            LoginResponseDto result = new LoginResponseDto();
            var user =
                _authenticationRepository.GetUserByUserNameOrNull(requestDto.Login);

            if ((user != null) &&
                IsPasswordsMatch(user, requestDto.Password))
            {
                result.AccessToken =
                    _tokenIssuerService.IssueJwtToken(
                        user.UserName,
                        user.Role.Title.ToLower());
                var refreshTokenData =
                    _tokenIssuerService.IssueRefreshToken(requestDto.Login);

                var refTokenEntity = _authenticationRepository.AddRefreshToken(
                    refreshTokenData.RefreshToken,
                    requestDto.Login,
                    refreshTokenData.ExpiredAt);

                result.RefreshToken = refreshTokenData.RefreshToken;
            }
            else
                throw new UnauthorizedAccessException();

            return result;
        }

        public LoginResponseDto RefreshToken(RefreshTokenDto requestDto)
        {
            var hash =
                _authenticationRepository.GetMD5Hash(requestDto.RefreshToken);

            var refreshTokenEntity =
                _authenticationRepository.GetRefreshTokenByHash(hash);

            if (refreshTokenEntity != null)
            {
                _unitOfWork.OpenTransaction();

                var user =
                    _authenticationRepository.GetUserByUserNameOrNull(refreshTokenEntity.UserName);

                var output = new LoginResponseDto();

                output.AccessToken =
                    _tokenIssuerService.IssueJwtToken(
                        user.UserName,
                        user.Role.Title.ToLower());
                var refreshTokenData =
                    _tokenIssuerService.IssueRefreshToken(user.UserName);

                var refTokenEntity = _authenticationRepository.AddRefreshToken(
                    refreshTokenData.RefreshToken,
                    user.UserName,
                    refreshTokenData.ExpiredAt);

                refreshTokenEntity.IsUsed = true;

                _unitOfWork.Commit();

                output.RefreshToken = refreshTokenData.RefreshToken;

                return output;
            }
            else throw new UnauthorizedAccessException();

            return new LoginResponseDto();
        }

        private bool IsPasswordsMatch(UserDb user, string password)
        {
            string hashedPass = HashPassword(password);

            if (hashedPass.Equals(user.Password)) return true;

            return false;
        }

        private string HashPassword(string password)
        {
            const int ITERATIONS_COUNT = 101;
            byte[] salt = Encoding.UTF8.GetBytes("&9dL3uFjAy9qf%QD");

            var deriveBytes =
                new Rfc2898DeriveBytes(password, salt, ITERATIONS_COUNT);
            byte[] hash = deriveBytes.GetBytes(32);

            return Convert.ToBase64String(hash);
        }
    }
}
