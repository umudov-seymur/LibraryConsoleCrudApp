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
    class GenreController : IController
    {
        /// <summary>
        /// Display a listing of the resource.
        /// </summary>
        public void Index()
        {
            UserConsole.WriteLine("\n[!] All Genres".ToUpper(), ConsoleColor.DarkYellow);

            using (var context = new LibraryContext())
            {
                var genres = context.Genres
                    .Include(n => n.BooksGenres)
                    .ThenInclude(n => n.Book);

                if (genres.Count() < 1)
                {
                    UserConsole.WriteLine("\n[!] No records found in the database.".ToUpper(), ConsoleColor.DarkRed);
                }

                foreach (var genre in genres)
                {
                    Show(genre);
                }
            }
        }

        /// <summary>
        /// Show the specified genre in database.
        /// </summary>
        public void Show(Genre genre)
        {
            var bookNames = genre.BooksGenres
                   .Select(u => u.Book.Name)
                   .ToList();

            Console.WriteLine(
                $"\nId: {genre.Id}" +
                $"\nName: {genre.Id}" +
                $"\nBooks: {String.Join(", ", bookNames)}"
            );
        }

        /// <summary>
        /// Store a newly created genre in database.
        /// </summary>
        public void Store()
        {
            UserConsole.WriteLine("\n[!] Create new genre".ToUpper(), ConsoleColor.DarkYellow);

            using (var context = new LibraryContext())
            {
                var validatedData = ValidatedData();

                Genre genreData = new Genre
                {
                    Name = validatedData["Genre name"]
                };

                context.Genres.Add(genreData);
                context.SaveChanges();

                UserConsole.WriteLine("\n[!] Genre created successfull.", ConsoleColor.Green);
            }
        }

        /// <summary>
        /// Update the specified genre in database.
        /// </summary>
        public void Update()
        {
            UserConsole.WriteLine("\n[!] Genre update operation".ToUpper(), ConsoleColor.DarkYellow);

            using (var context = new LibraryContext())
            {
            IsExist:
                string genred = InputValidation.Validate("Genre Id", new Predicate<string>[] {
                        Validation.IsRequired,
                        Validation.IsPositiveInteger
                    });

                var genre = context.Genres
                        .Include(a => a.BooksGenres)
                        .Where(a => a.Id == int.Parse(genred))
                        .FirstOrDefault();

                if (genre != null)
                {
                    Show(genre);

                    var validatedData = ValidatedData();

                    genre.Name = validatedData["Genre name"];

                    context.Genres.Update(genre);
                    context.SaveChanges();

                    UserConsole.WriteLine("\n[!] Genre updated successfull.", ConsoleColor.Green);

                    return;
                }

                UserConsole.WriteLine("There is no such record, enter a correct id.", ConsoleColor.Red);
                goto IsExist;
            }
        }

        // <summary>
        /// Remove the specified genre from database.
        /// </summary>
        public void Destroy()
        {
            UserConsole.WriteLine("\n[!] Genre delete operation".ToUpper(), ConsoleColor.DarkYellow);

            using (var context = new LibraryContext())
            {
            IsExist:
                string genreId = InputValidation.Validate("Genre Id", new Predicate<string>[] {
                        Validation.IsRequired,
                        Validation.IsPositiveInteger
                    });

                var genre = context.Genres
                        .Include(n => n.BooksGenres)
                        .Where(a => a.Id == int.Parse(genreId))
                        .FirstOrDefault();

                if (genre != null)
                {
                    Show(genre);
                    context.Genres.Remove(genre);
                    context.SaveChanges();
                    UserConsole.WriteLine("\n[!] Genre deleted successfull.", ConsoleColor.Green);
                    return;
                }

                UserConsole.WriteLine("There is no such record, enter a correct id.", ConsoleColor.Red);
                goto IsExist;
            }
        }

        /// <summary>
        /// Check genre name is db exist (for unique)
        /// </summary>
        /// <param name="genreName"></param>
        /// <returns></returns>
        public static bool IsExistGenre(string genreName)
        {
            using (var context = new LibraryContext())
            {
                var genre = context.Genres
                        .Where(a => a.Name.Trim().ToLower() == genreName.Trim().ToLower())
                        .FirstOrDefault();

                if (genre != null)
                {
                    throw new GenreAlreadyExistException("The genre has already been stored.");
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
                Validation.IsValidGenreName,
                IsExistGenre
            };

            return InputValidation.Validate(
                new Dictionary<string, Predicate<string>[]>{
                    {"Genre name", validationRules }
                }
            );
        }
    }
}
