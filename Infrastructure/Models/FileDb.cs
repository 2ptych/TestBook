using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class FileDb
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Hash { get; set; }
        //
        public virtual List<BookFileJoinDb> BookFileJoin { get; set; }
    }
}
