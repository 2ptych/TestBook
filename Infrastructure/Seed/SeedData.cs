using Infrastructure.Context;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Seed
{
    public class SeedData
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.Migrate();

            if (context.Users.Any()) return;

            context.Users.AddRange(SeedUsers);
            context.SaveChanges();
        }

        private static List<UserDb> SeedUsers = new List<UserDb>
        {
            new UserDb
            {
                UserName = "Admin",
                // pass admin
                Password = "UqUhUpTUjumX5a3RcezwDVVRmCNcTM9X4HKiDGMxoMQ=",
                Role = new RoleDb
                {
                    Title = "Admin"
                }
            },
            new UserDb
            {
                UserName = "User",
                // pass user
                Password = "PhM90Zr5YaKC9/Xh5dxdvapxTgo1QLLqjBPLTeTR03o=",
                Role = new RoleDb
                {
                    Title = "User"
                }
            }
        };
    }
}
