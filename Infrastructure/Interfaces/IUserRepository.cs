using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        void AddBookToUserFavorites(BookDb book, UserDb user);
        void DeleteBookFromUserFavorites(BookDb book, UserDb user);
    }
}
