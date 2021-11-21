using System;
using System.Linq;
using Utilities.Utils;
using Utilities.Validations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LibraryConsoleCrudApp.Models.Context;
using LibraryConsoleCrudApp.Models.Entities;
using LibraryConsoleCrudApp.Business.Abstract;

namespace LibraryConsoleCrudApp.Controllers
{
    class BookController : IController
    {
        /// <summary>
        /// Display a listing of the resource.
        /// </summary>
        public void Index()
        {
            UserConsole.WriteLine("\n[!] All books".ToUpper(), ConsoleColor.DarkYellow);

            using (var context = new LibraryContext())
            {
                var books = context.Books
                        .Include(d => d.Author)
                        .Include(n => n.BooksGenres)
                        .ThenInclude(m => m.Genre);

                if (books.Count() < 1)
                {
                    UserConsole.WriteLine("\n[!] No records found in the database.".ToUpper(), ConsoleColor.DarkRed);
                }

                foreach (var book in books)
                {
                    var genres = book.BooksGenres
                      .Select(u => u.Genre.Name)
                      .ToList();

                    string pluckGenresName = String.Join(", ", genres);

                    Console.WriteLine(
                        $"\nId: {book.Id}\n" +
                        $"Title: {book.Name}\n" +
                        $"Author: {book.Author.Name}\n" +
                        $"Genres: {pluckGenresName}\n" +
                        $"ISBN: {book.Isbn}\n" +
                        $"Edition: {book.Edition}"
                    );
                }
            }
        }

        /// <summary>
        /// Store a newly created book in database.
        /// </summary>
        public void Store()
        {
            UserConsole.WriteLine("\n[!] Create new book".ToUpper(), ConsoleColor.DarkYellow);

            new AuthorController().Index();

            using (var context = new LibraryContext())
            {
                var validatedData = ValidatedData();

                Author author = context.Authors
                     .Where(a => a.Name.Trim().ToLower() == validatedData["Author name"].Trim().ToLower())
                     .FirstOrDefault();

                Publisher publisher = context.Publishers
                     .Where(a => a.Name.Trim().ToLower() == validatedData["Publisher name"].Trim().ToLower())
                    .FirstOrDefault();

                author = author != null
                    ? author
                    : new Author { Name = validatedData["Author name"] };

                publisher = publisher != null
                     ? publisher
                     : new Publisher { Name = validatedData["Publisher name"] };

                Book bookData = new Book
                {
                    Name = validatedData["Book title"],
                    Author = author,
                    Publisher = publisher,
                    Isbn = validatedData["Book isbn"],
                    Edition = int.Parse(validatedData["Book edition"]),
                };

                context.Books.Add(bookData);
                context.SaveChanges();

                UserConsole.WriteLine("\n[!] Book created successfull.", ConsoleColor.Green);
            }
        }

        /// <summary>
        /// Update the specified book in database.
        /// </summary>
        public void Update()
        {
            //
        }

        // <summary>
        /// Destroy the specified genre from database.
        /// </summary>
        public void Destroy()
        {
            UserConsole.WriteLine("\n[!] Book delete operation".ToUpper(), ConsoleColor.DarkYellow);

            using (var context = new LibraryContext())
            {
            IsExist:
                string bookId = InputValidation.Validate("Book Id", new Predicate<string>[] {
                    Validation.IsRequired,
                    Validation.IsPositiveInteger
                });

                var book = context.Books
                        .Where(book => book.Id == int.Parse(bookId))
                        .FirstOrDefault();

                if (book != null)
                {
                    context.Books.Remove(book);
                    context.SaveChanges();
                    UserConsole.WriteLine("Book deleted successfull.", ConsoleColor.Green);
                    return;
                }

                UserConsole.WriteLine("No record found for this id.", ConsoleColor.Red);
                goto IsExist;
            }
        }

        /// <summary>
        /// Get validated fields data
        /// </summary>
        /// <returns>Fields</returns>
        public Dictionary<string, string> ValidatedData()
        {
            var validationRules = new Predicate<string>[] {
                Validation.IsRequired,
                Validation.IsValidAuthorName
            };

            return InputValidation.Validate(
                new Dictionary<string, Predicate<string>[]>{
                    {"Book title", validationRules },
                    {"Author name", validationRules },
                    {"Publisher name", validationRules },
                    {"Book isbn",  new Predicate<string>[] { Validation.IsRequired }},
                    {"Book edition", new Predicate<string>[] { Validation.IsRequired, Validation.IsPositiveInteger }},
                }
            );
        }
    }
}
