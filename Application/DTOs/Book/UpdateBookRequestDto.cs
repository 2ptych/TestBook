using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class UpdateBookRequestDto : AddBookRequestDto
    {
        public int Id { get; set; }
    }
}
