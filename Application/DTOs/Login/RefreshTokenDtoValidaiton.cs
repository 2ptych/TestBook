using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.DTOs.Login
{
    public class RefreshTokenDtoValidaiton : AbstractValidator<RefreshTokenDto>
    {
        public RefreshTokenDtoValidaiton()
        {
            RuleFor(x => x)
                .Must(m =>
                {
                    Regex refreshFormat =
                        new Regex("^[0-9,a-f]{128}$",
                            RegexOptions.IgnoreCase);

                    int cnt = m.RefreshToken.Length;

                    if (refreshFormat.IsMatch(m.RefreshToken))
                        return true;

                    return false;
                })
                .WithMessage("Неверный формат RefreshToken")
                .WithName("RefreshToken");
        }
    }
}
