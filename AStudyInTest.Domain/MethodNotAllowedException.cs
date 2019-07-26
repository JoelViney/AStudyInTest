using System;
using System.Collections.Generic;
using System.Text;

namespace AStudyInTest.Domain
{
    /// <summary>
    /// This exception is thrown when the user attempts to perform an action that they don't have permission to do.
    /// </summary>
    public class MethodNotAllowedException : Exception
    {
        public MethodNotAllowedException() : base("The request cannot be completed.")
        {

        }

        public MethodNotAllowedException(string message) : base(message)
        {

        }
    }
}
