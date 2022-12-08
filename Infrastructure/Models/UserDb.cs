using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class UserDb
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public virtual RoleDb Role { get; set; }
        public string Password { get; set; }
        public List<UserFavoritesJoinDb> UserFavoritesJoin { get; set; }
    }
}
