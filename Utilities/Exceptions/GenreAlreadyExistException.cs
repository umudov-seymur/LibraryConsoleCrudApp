using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Exceptions
{
    public class GenreAlreadyExistException : Exception
    {
        public GenreAlreadyExistException(string message) : base(message)
        {

        }
    }
}
