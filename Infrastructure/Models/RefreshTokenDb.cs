using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class RefreshTokenDb
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string TokenHash { get; set; }
        public DateTime ExpiredAt { get; set; }
        public bool IsRevoked { get; set; }
        public bool IsUsed { get; set; }
    }
}
