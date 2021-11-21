using System.Text.RegularExpressions;
using Utilities.Exceptions;

namespace Utilities.Validations
{
    public class Validation
    {
        /// <summary>
        /// Checks that the entered string is null or space
        /// </summary>
        /// <param name="str"></param>
        /// <returns>true if it matches; if not match return IsRequiredException</returns>
        public static bool IsRequired(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new IsRequiredException($"This field is required.");
            }

            return true;
        }

        /// <summary>
        /// Checks that the entered author name
        /// </summary>
        /// <param name="str"></param>
        /// <returns>true if it matches; if not match return NotValidAuthorNameException</returns>
        public static bool IsValidAuthorName(string str)
        {
            if (!Regex.IsMatch(str, @"^[A-Za-z ,-]+$"))
            {
                throw new NotValidAuthorNameException($"This author name is not valid. [pattern accept (a-zA-Z ,-)]");
            }

            return true;
        }

        /// <summary>
        /// Checks that the entered genre name
        /// </summary>
        /// <param name="str"></param>
        /// <returns>true if it matches; if not match return NotValidGenreNameException</returns>
        public static bool IsValidGenreName(string str)
        {
            if (!Regex.IsMatch(str, @"^[A-Za-z ]+$"))
            {
                throw new NotValidGenreNameException($"This genre name is not valid. [pattern accept (a-zA-Z )]");
            }

            return true;
        }

        /// <summary>
        /// Checks that the entered publisher name
        /// </summary>
        /// <param name="str"></param>
        /// <returns>true if it matches; if not match return NotValidPublisherNameException</returns>
        public static bool IsValidPublisherName(string str)
        {
            if (!Regex.IsMatch(str, @"^[A-Za-z ]+$"))
            {
                throw new NotValidPublisherNameException($"This publisher name is not valid. [pattern accept (a-zA-Z )]");
            }

            return true;
        }

        /// <summary>
        /// Checks that the entered number is integere and positive
        /// </summary>
        /// <param name="str"></param>
        /// <returns>true if it matches; if not match return NotValidPublisherNameException</returns>
        public static bool IsPositiveInteger(string str)
        {
            if (!Regex.IsMatch(str, @"^[0-9]*[1-9][0-9]*$"))
            {
                throw new NotPositiveNumberException($"This field is only positive integer number.");
            }

            return true;
        }
    }
}
