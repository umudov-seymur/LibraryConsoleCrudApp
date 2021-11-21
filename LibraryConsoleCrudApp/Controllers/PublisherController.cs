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
    class PublisherController : IController
    {
        /// <summary>
        /// Display a listing of the resource.
        /// </summary>
        public void Index()
        {
            UserConsole.WriteLine("\n[!] All Publisher".ToUpper(), ConsoleColor.DarkYellow);

            using (var context = new LibraryContext())
            {
                var publishers = context.Publishers
                    .Include(n => n.Books);

                if (publishers.Count() < 1)
                {
                    UserConsole.WriteLine("\n[!] No records found in the database.".ToUpper(), ConsoleColor.DarkRed);
                }

                foreach (var publisher in publishers)
                {
                    Show(publisher);
                }
            }
        }

        /// <summary>
        /// Show the specified publisher in database.
        /// </summary>
        public void Show(Publisher publisher)
        {
            var bookNames = publisher.Books
                   .Select(u => u.Name)
                   .ToList();

            Console.WriteLine(
                $"\nId: {publisher.Id}" +
                $"\nName: {publisher.Id}" +
                $"\nBooks: {String.Join(", ", bookNames)}"
            );
        }

        /// <summary>
        /// Store a newly created publisher in database.
        /// </summary>
        public void Store()
        {
            UserConsole.WriteLine("\n[!] Create new publisher".ToUpper(), ConsoleColor.DarkYellow);

            using (var context = new LibraryContext())
            {
                var validatedData = ValidatedData();

                Publisher publisherData = new Publisher
                {
                    Name = validatedData["Publisher name"]
                };

                context.Publishers.Add(publisherData);
                context.SaveChanges();

                UserConsole.WriteLine("\n[!] Publisher created successfull.", ConsoleColor.Green);
            }
        }

        /// <summary>
        /// Update the specified publisher in database.
        /// </summary>
        public void Update()
        {
            throw new NotImplementedException();
        }

        // <summary>
        /// Remove the specified publisher from database.
        /// </summary>
        public void Destroy()
        {
            UserConsole.WriteLine("\n[!] Publisher delete operation".ToUpper(), ConsoleColor.DarkYellow);

            using (var context = new LibraryContext())
            {
            IsExist:
                string publisherId = InputValidation.Validate("Publisher Id", new Predicate<string>[] {
                        Validation.IsRequired,
                        Validation.IsPositiveInteger
                    });

                var publisher = context.Publishers
                        .Where(a => a.Id == int.Parse(publisherId))
                        .FirstOrDefault();

                if (publisher != null)
                {
                    context.Publishers.Remove(publisher);
                    context.SaveChanges();
                    UserConsole.WriteLine("\n[!] Publisher deleted successfull.", ConsoleColor.Green);
                    return;
                }

                UserConsole.WriteLine("There is no such record, enter a correct id.", ConsoleColor.Red);
                goto IsExist;
            }
        }

        /// <summary>
        /// Check publisher name is db exist (for unique)
        /// </summary>
        /// <param name="publisherName"></param>
        /// <returns></returns>
        public static bool IsExistPublisher(string publisherName)
        {
            using (var context = new LibraryContext())
            {
                var publisher = context.Publishers
                        .Include(n => n.Books)
                        .Where(a => a.Name.Trim().ToLower() == publisherName.Trim().ToLower())
                        .FirstOrDefault();

                if (publisher != null)
                {
                    throw new PublisherAlreadyExistException("The publisher has already been stored.");
                }

                return true;
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
                Validation.IsValidPublisherName,
                IsExistPublisher
            };

            return InputValidation.Validate(
                new Dictionary<string, Predicate<string>[]>{
                    {"Publisher name", validationRules }
                }
            );
        }
    }
}
