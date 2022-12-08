using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class SearchBooksRequestDto
    {
        public string SearchString { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
