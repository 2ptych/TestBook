using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class RefreshTokenData
    {
        public string RefreshToken { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
