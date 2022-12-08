using FluentValidation;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.User
{
    public class UserFavoritesHandleDtoValidator :
        AbstractValidator<UserFavoritesHandleDto>
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IBookRepository _bookRepository;

        private bool _validBookId = false;
        private bool _validUserId = false;

        public UserFavoritesHandleDtoValidator(
            IAuthenticationRepository authenticationRepository,
            IBookRepository bookRepository)
        {
            _authenticationRepository = authenticationRepository;
            _bookRepository = bookRepository;

            RuleFor(x => x)
                .Must(m =>
                {
                    if (m.BookId.HasValue) _validBookId = true;

                    return _validBookId;
                })
                .WithMessage("Укажите BookId")
                .WithName("BookId");

            RuleFor(x => x)
                .Must(m =>
                {
                    if (m.UserId.HasValue) _validUserId = true;

                    return _validUserId;
                })
                .WithMessage("Укажите UserId")
                .WithName("UserId");

            When(x => _validBookId && _validUserId, () =>
            {
                RuleFor(x => x)
                    .Must(m =>
                    {
                        m.User =
                            _authenticationRepository.GetUserById(m.UserId.Value);

                        if (m.User != null) return true;

                        return false;
                    })
                    .WithMessage("Пользователя с таким Id не существует")
                    .WithName("UserId");

                RuleFor(x => x)
                    .Must(m =>
                    {
                        m.Book =
                            _bookRepository.GetById(m.UserId.Value);

                        if (m.Book != null) return true;

                        return false;
                    })
                    .WithMessage("Книги с таким Id не существует")
                    .WithName("BookId");
            });
        }
    }
}
