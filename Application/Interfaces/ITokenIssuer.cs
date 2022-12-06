using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ITokenIssuerService
    {
        string IssueJwtToken(string userId, string role);
        RefreshTokenData IssueRefreshToken(string userId);
    }
}
