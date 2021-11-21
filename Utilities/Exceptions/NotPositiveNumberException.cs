using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Exceptions
{
    public class NotPositiveNumberException : Exception
    {
        public NotPositiveNumberException(string message) : base(message)
        {

        }
    }
}
