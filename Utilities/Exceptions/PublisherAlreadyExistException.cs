using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Exceptions
{
    public class PublisherAlreadyExistException : Exception
    {
        public PublisherAlreadyExistException(string message) : base(message)
        {

        }
    }
}
