using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Exceptions
{
    public class IsRequiredException : Exception
    {
        public IsRequiredException(string message) : base(message)
        {

        }
    }
}
