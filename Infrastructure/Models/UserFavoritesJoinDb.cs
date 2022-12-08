using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class UserFavoritesJoinDb
    {
        public int BookId { get; set; }
        public BookDb Book { get; set; }
        public int UserId { get; set; }
        public UserDb User { get; set; }
    }
}
