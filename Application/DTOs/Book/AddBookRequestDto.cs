using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class AddBookRequestDto
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public int Year { get; set; }
        public IFormFile File { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<int> AuthorIds { get; set; }
    }
}
