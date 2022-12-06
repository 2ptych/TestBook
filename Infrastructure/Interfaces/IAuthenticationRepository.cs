using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces
{
    public interface IAuthenticationRepository
    {
        UserDb GetUserByUserNameOrNull(string userName);
        void AddRefreshToken(
            string token, string userName, DateTime expiredAt);
    }
}
