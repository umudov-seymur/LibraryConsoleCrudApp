using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Exceptions
{
    public class AuthorAlreadyExistException : Exception
    {
        public AuthorAlreadyExistException(string message) : base(message)
        {

        }
    }
}
