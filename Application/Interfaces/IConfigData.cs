using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IConfigData
    {
        string GetSigningKeyString();
        DateTime GetTimestampAccessTokenExpiredAt();
        string GetIssuer();
        string GetAudience();
        DateTime GetTimestampRefreshTokenExpiredAt();
    }
}
