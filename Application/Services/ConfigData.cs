using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ConfigData : IConfigData
    {
        private readonly IConfiguration _configuration;

        const int EXP_TOKEN_TIME_MULTIPLICATOR = 60;

        public ConfigData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetSigningKeyString()
        {
            return _configuration["JwtOptions:SigningKey"];
        }

        public string GetIssuer()
        {
            return _configuration["JwtOptions:ValidIssuer"];
        }

        public string GetAudience()
        {
            return _configuration["JwtOptions:ValidAudience"];
        }

        public DateTime GetTimestampAccessTokenExpiredAt()
        {
            
            int minutes =
                Int32.Parse(_configuration["JwtOptions:AccessExpiredInMinutes"]);
            int secondsTokenExpIn = EXP_TOKEN_TIME_MULTIPLICATOR * minutes;
            return DateTime.Now.Add(new TimeSpan(0, 0, secondsTokenExpIn));
        }

        public DateTime GetTimestampRefreshTokenExpiredAt()
        {

            int minutes =
                Int32.Parse(_configuration["JwtOptions:RefreshExpiredInMinutes"]);
            int secondsTokenExpIn = EXP_TOKEN_TIME_MULTIPLICATOR * minutes;
            return DateTime.Now.Add(new TimeSpan(0, 0, secondsTokenExpIn));
        }

        private string DateTimeToUnixSeconds(DateTime date)
        {
            long unixTimestamp =
                (long)date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return unixTimestamp.ToString();
        }
    }
}
