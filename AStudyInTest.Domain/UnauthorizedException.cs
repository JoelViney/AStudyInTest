using System;
using System.Collections.Generic;
using System.Text;

namespace AStudyInTest.Domain
{
    /// <summary>
    /// This exception is thrown when the user attempts to perform an action that they don't have permission to do.
    /// </summary>
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("Invalid attempt to access a resource.")
        {

        }

        public UnauthorizedException(string message) : base(message)
        {

        }
    }
}
