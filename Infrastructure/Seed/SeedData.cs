﻿using Infrastructure.Context;
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

            context.Category.AddRange(Categories);
            context.Author.AddRange(Authors);
            context.Book.AddRange(new List<BookDb>
            {
                Book1,
                Book2,
                Book3
            });
            context.Users.AddRange(Users);
            context.SaveChanges();
        }

        private static List<CategoryDb> Categories = new List<CategoryDb>
        {
            new CategoryDb
            {
                Title = "Computer Science"
            },
            new CategoryDb
            {
                Title = "Разработка программного обеспечения"
            },
            new CategoryDb
            {
                Title = "Программирование"
            },
            new CategoryDb
            {
                Title = "Операционные системы"
            }
        };

        private static List<AuthorDb> Authors = new List<AuthorDb>
        {
            new AuthorDb
            {
                FullName = "Мартин Фаулер"
            },
            new AuthorDb
            {
                FullName = "Девид Райс"
            },
            new AuthorDb
            {
                FullName = "Меттью Фоммел"
            },
            new AuthorDb
            {
                FullName = "Эдвард Хайет"
            },
            new AuthorDb
            {
                FullName = "Эндрю Танненбаум"
            },
            new AuthorDb
            {
                FullName = "Роберт Мартин"
            }
        };

        private static BookDb Book1 = new BookDb
        {
            Year = 2002,
            Description = "",
            Title = "Шаблоны корпоративных приложений",
            PageCount = 548,
            BookAuthorJoin = new List<BookAuthorJoinDb>
            {
                new BookAuthorJoinDb
                {
                    Book = Book1,
                    Author = Authors[0],
                    Order = 1
                },
                new BookAuthorJoinDb
                {
                    Book = Book1,
                    Author = Authors[1],
                    Order = 2
                },
                new BookAuthorJoinDb
                {
                    Book = Book1,
                    Author = Authors[2],
                    Order = 3
                },
                new BookAuthorJoinDb
                {
                    Book = Book1,
                    Author = Authors[3],
                    Order = 4
                }
            },
            BookCategoryJoin = new List<BookCategoryJoin>
            {
                new BookCategoryJoin
                {
                    Book = Book3,
                    Category = Categories[1]
                },
                new BookCategoryJoin
                {
                    Book = Book3,
                    Category = Categories[2]
                }
            }
        };

        private static BookDb Book2 = new BookDb
        {
            Year = 2018,
            Description = "",
            Title = "Чистая архитектура. Искусство разработки программного обеспечения",
            PageCount = 410,
            BookAuthorJoin = new List<BookAuthorJoinDb>
            {
                new BookAuthorJoinDb
                {
                    Book = Book2,
                    Author = Authors[5],
                    Order = 1
                }
            },
            BookCategoryJoin = new List<BookCategoryJoin>
            {
                new BookCategoryJoin
                {
                    Book = Book3,
                    Category = Categories[1]
                },
                new BookCategoryJoin
                {
                    Book = Book3,
                    Category = Categories[2]
                }
            }
        };

        private static BookDb Book3 = new BookDb
        {
            Year = 2019,
            Description = "",
            Title = "Современные операционные системы",
            PageCount = 1120,
            BookAuthorJoin = new List<BookAuthorJoinDb>
            {
                new BookAuthorJoinDb
                {
                    Book = Book2,
                    Author = Authors[4],
                    Order = 1
                }
            },
            BookCategoryJoin = new List<BookCategoryJoin>
            {
                new BookCategoryJoin
                {
                    Book = Book3,
                    Category = Categories[0]
                },
                new BookCategoryJoin
                {
                    Book = Book3,
                    Category = Categories[3]
                }
            }
        };

        private static List<UserDb> Users = new List<UserDb>
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
