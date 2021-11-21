using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Exceptions
{
    public class NotValidAuthorNameException : Exception
    {
        public NotValidAuthorNameException(string message) : base(message)
        {

        }
    }
}
