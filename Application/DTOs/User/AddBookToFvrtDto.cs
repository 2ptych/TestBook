using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Application.DTOs
{
    public class AddBookToFvrtDto : UserFavoritesHandleDto
    {
        
    }

    public class UserFavoritesHandleDto
    {
        public int? UserId { get; set; }
        public int? BookId { get; set; }

        [JsonIgnore]
        public UserDb User { get; set; }
        [JsonIgnore]
        public BookDb Book { get; set; }
    }
}
