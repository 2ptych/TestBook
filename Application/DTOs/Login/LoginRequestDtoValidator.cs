using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Login
{
    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            RuleFor(x => x.Login)
                .Matches("^.{1,100}$")
                .WithMessage("Введите логин")
                .WithName("Login");

            RuleFor(x => x.Password)
                .Matches("^.{1,100}$")
                .WithMessage("Введите пароль")
                .WithName("Password");
        }
    }
}
