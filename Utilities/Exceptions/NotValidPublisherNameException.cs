using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Exceptions
{
    public class NotValidPublisherNameException : Exception
    {
        public NotValidPublisherNameException(string message) : base(message)
        {

        }
    }
}
