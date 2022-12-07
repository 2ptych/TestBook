using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AppDbContext _context;

        public AuthenticationRepository(AppDbContext context)
        {
            _context = context;
        }

        public UserDb GetUserByUserNameOrNull(string userName)
        {
            UserDb user = _context.Users
                .Where(x => x.UserName == userName)
                .Include(x => x.Role)
                .FirstOrDefault();

            return user;
        }

        public void AddRefreshToken(string token, string userName, DateTime expiredAt)
        {
            RefreshTokenDb refreshToken = new RefreshTokenDb
            {
                ExpiredAt = expiredAt,
                IsRevoked = false,
                IsUsed = false,
                UserName = userName,
                Token = token,
                TokenHash = GetMD5Hash(token)
            };

            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();
        }

        public RefreshTokenDb GetRefreshTokenByHash(string hash)
        {
            var token = _context.RefreshTokens
                .Where(x =>
                    (x.IsRevoked == false) &&
                    (x.IsUsed == false))
                .Where(x => x.TokenHash == hash)
                .FirstOrDefault();

            return token;
        }

        public string GetMD5Hash(string str)
        {
            byte[] inputBytes;
            byte[] hashBytes;
            using (var md5 = MD5.Create())
            {
                inputBytes = Encoding.UTF8.GetBytes(str);
                hashBytes = md5.ComputeHash(inputBytes);
            }
            
            var sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString().ToLower();
        }
    }
}
