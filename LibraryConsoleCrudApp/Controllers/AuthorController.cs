using System;
using System.Linq;
using Utilities.Utils;
using Utilities.Exceptions;
using Utilities.Validations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LibraryConsoleCrudApp.Models.Context;
using LibraryConsoleCrudApp.Models.Entities;
using LibraryConsoleCrudApp.Business.Abstract;

namespace LibraryConsoleCrudApp.Controllers
{
    class AuthorController : IController
    {
        /// <summary>
        /// Display a listing of the resource.
        /// </summary>
        public void Index()
        {
            UserConsole.WriteLine("\n[!] All Authors".ToUpper(), ConsoleColor.DarkYellow);

            using (var context = new LibraryContext())
            {
                var authors = context.Authors
                    .Include(n => n.Books);

                if (authors.Count() < 1)
                {
                    UserConsole.WriteLine("\n[!] No records found in the database.".ToUpper(), ConsoleColor.DarkRed);
                }

                foreach (var author in authors)
                {
                    Show(author);
                }
            }
        }

        /// <summary>
        /// Show the specified author in database.
        /// </summary>
        public void Show(Author author)
        {
            var bookNames = author.Books
                   .Select(u => u.Name)
                   .ToList();

            Console.WriteLine(
                $"\nId: {author.Id}" +
                $"\nAuthor name: {author.Name}" +
                $"\nBooks: {String.Join(", ", bookNames)}"
            );
        }

        /// <summary>
        /// Store a newly created author in database.
        /// </summary>
        public void Store()
        {
            UserConsole.WriteLine("\n[!] Create new author".ToUpper(), ConsoleColor.DarkYellow);

            using (var context = new LibraryContext())
            {
                var validatedData = ValidatedData();

                Author authorData = new Author
                {
                    Name = validatedData["Author name"]
                };

                context.Authors.Add(authorData);
                context.SaveChanges();

                UserConsole.WriteLine("\n[!] Author created successfull.", ConsoleColor.Green);
            }
        }

        /// <summary>
        /// Update the specified author in database.
        /// </summary>
        public void Update()
        {
            UserConsole.WriteLine("\n[!] Author update operation".ToUpper(), ConsoleColor.DarkYellow);

            using (var context = new LibraryContext())
            {
            IsExist:
                string authorId = InputValidation.Validate("Author Id", new Predicate<string>[] {
                        Validation.IsRequired,
                        Validation.IsPositiveInteger
                    });

                var author = context.Authors
                        .Include(a => a.Books)
                        .Where(a => a.Id == int.Parse(authorId))
                        .FirstOrDefault();

                if (author != null)
                {
                    Show(author);

                    var validatedData = ValidatedData();

                    author.Name = validatedData["Author name"];

                    context.Authors.Update(author);
                    context.SaveChanges();

                    UserConsole.WriteLine("\n[!] Author updated successfull.", ConsoleColor.Green);

                    return;
                }

                UserConsole.WriteLine("There is no such record, enter a correct id.", ConsoleColor.Red);
                goto IsExist;
            }
        }

        // <summary>
        /// Remove the specified author from database.
        /// </summary>
        public void Destroy()
        {
            UserConsole.WriteLine("\n[!] Author delete operation".ToUpper(), ConsoleColor.DarkYellow);

            using (var context = new LibraryContext())
            {
            IsExist:
                string authorId = InputValidation.Validate("Author Id", new Predicate<string>[] {
                        Validation.IsRequired,
                        Validation.IsPositiveInteger
                    });

                var author = context.Authors
                        .Where(a => a.Id == int.Parse(authorId))
                        .FirstOrDefault();

                if (author != null)
                {
                    context.Authors.Remove(author);
                    context.SaveChanges();
                    UserConsole.WriteLine("\n[!] Author deleted successfull.", ConsoleColor.Green);
                    return;
                }

                UserConsole.WriteLine("There is no such record, enter a correct id.", ConsoleColor.Red);
                goto IsExist;
            }
        }

        /// <summary>
        /// Check genre author is db exist (for unique)
        /// </summary>
        /// <param name="authorName"></param>
        /// <returns></returns>
        public static bool IsExistAuthor(string authorName)
        {
            using (var context = new LibraryContext())
            {
                var author = context.Authors
                        .Where(a => a.Name.Trim().ToLower() == authorName.Trim().ToLower())
                        .FirstOrDefault();

                if (author != null)
                {
                    throw new AuthorAlreadyExistException("The author has already been stored.");
                }

                return true;
            }
        }

        /// <summary>
        /// Get validated fields data
        /// </summary>
        /// <returns>Fields</returns>
        private Dictionary<string, string> ValidatedData()
        {
            var validationRules = new Predicate<string>[] {
                Validation.IsRequired,
                Validation.IsValidAuthorName,
                IsExistAuthor
            };

            return InputValidation.Validate(
                new Dictionary<string, Predicate<string>[]>{
                    {"Author name", validationRules }
                }
            );
        }
    }
}
