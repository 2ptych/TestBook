using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class BookDb
    {
        public int Id { get; set; }
        public int PageCount { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public virtual FileDb Cover { get; set; }

        // navigational
        public virtual List<BookAuthorJoinDb> BookAuthorJoin { get; set; }
        //public virtual List<BookFileJoinDb> BookFileJoin { get; set; }
        public virtual List<BookCategoryJoin> BookCategoryJoin { get; set; }
    }
}
