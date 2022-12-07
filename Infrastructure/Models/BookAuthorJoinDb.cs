using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class BookAuthorJoinDb
    {
        public int BookId { get; set; }
        public virtual BookDb Book { get; set; }
        public int AuthorId { get; set; }
        public virtual AuthorDb Author { get; set; }
        public int Order { get; set; }
    }
}
