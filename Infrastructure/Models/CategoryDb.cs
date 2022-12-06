using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class CategoryDb
    {
        public int Id { get; set; }
        public string Title { get; set; }
        //
        public virtual List<BookCategoryJoin> BookCategoryJoin { get; set; }
    }
}
