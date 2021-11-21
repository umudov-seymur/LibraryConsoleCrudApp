using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Exceptions
{
    public class NotValidGenreNameException : Exception
    {
        public NotValidGenreNameException(string message) : base(message)
        {

        }
    }
}
