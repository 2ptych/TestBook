using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class BookFileJoinDb
    {
        public int FileId { get; set; }
        public virtual FileDb File { get; set; }
        public int BookId { get; set; }
        public virtual BookDb Book { get; set; }
    }
}
