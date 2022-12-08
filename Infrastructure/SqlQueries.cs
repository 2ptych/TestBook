using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class SqlQueries
    {
        public readonly static string BookSearchByTitle =
            "Select * from \"Book\" bk " +
            "where bk.\"Title\" ILIKE '%{0}%' ";

        public readonly static string BookSearchByTitleWithCategories =
            "Select " +
                "bk.\"Id\" " +
            "from " +
                "(Select * from \"BookCategoryJoin\" bcj " +
                "where bcj.\"CategoryId\" in ({1})) x " +
            "inner join \"Book\" bk " +
            "on x.\"BookId\" = bk.\"Id\" " +
            "where bk.\"Title\" iLike '%{0}%' ";
    }
}
