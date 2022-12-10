using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class SearchBooksResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<SBObject> Categories { get; set; }
        public List<SBObject> Authors { get; set; }
        public string Cover { get; set; }
    }

    public class SBObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
