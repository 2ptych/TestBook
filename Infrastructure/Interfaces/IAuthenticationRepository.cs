using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces
{
    public interface IAuthenticationRepository
    {
        UserDb GetUserByUserNameOrNull(string userName);
        UserDb GetUserById(int id);
        RefreshTokenDb AddRefreshToken(
            string token, string userName, DateTime expiredAt);
        string GetMD5Hash(string str);
        RefreshTokenDb GetRefreshTokenByHash(string hash);
    }
}
