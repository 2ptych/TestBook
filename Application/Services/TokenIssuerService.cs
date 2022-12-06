using Application.Interfaces;
using Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class TokenIssuerService : ITokenIssuerService
    {
        private readonly IConfigData _configData;
        private static RandomNumberGenerator rgn = RandomNumberGenerator.Create();

        public TokenIssuerService(IConfigData configData)
        {
            _configData = configData;
        }

        public string IssueJwtToken(string userId, string role)
        {
            string signingKey = _configData.GetSigningKeyString();
            var keyBytes = Encoding.UTF8.GetBytes(signingKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("role", role),
                    new Claim(JwtRegisteredClaimNames.Sub, userId)
                }),
                Audience = _configData.GetAudience(),
                Issuer = _configData.GetIssuer(),
                Expires = _configData.GetTimestampAccessTokenExpiredAt(),
                IssuedAt = DateTime.Now,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var resultJwtToken = jwtTokenHandler.WriteToken(token);

            return resultJwtToken;
        }

        // Выпустить рефреш токен для 
        public RefreshTokenData IssueRefreshToken(string userId)
        {
            var expiredAt = _configData.GetTimestampRefreshTokenExpiredAt();

            int size = 64;
            var rndBytes = GenerateRandomBytes(size);
            string token = string.Empty;

            for (int i = 0; i < size; i++)
            {
                token += rndBytes[i].ToString("x2");
            }

            return new RefreshTokenData
            {
                ExpiredAt = expiredAt,
                RefreshToken = token
            };
        }

        private byte[] GenerateRandomBytes(int size)
        {
            byte[] randomNum = new byte[64];
            rgn.GetBytes(randomNum);
            return randomNum; //Convert.ToBase64String(randomNum);
        }
    }
}
