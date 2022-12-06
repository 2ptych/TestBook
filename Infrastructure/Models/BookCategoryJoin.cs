using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class BookCategoryJoin
    {
        public int BookId { get; set; }
        public virtual BookDb Book { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryDb Category { get; set; }
    }
}
