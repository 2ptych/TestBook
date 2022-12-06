using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class AuthorDb
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        // navigational
        public virtual List<BookAuthorJoinDb> BookAuthorJoin { get; set; }
    }
}
