using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class RefreshTokenDto
    {
        //[JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
