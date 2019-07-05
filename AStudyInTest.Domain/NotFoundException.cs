using System;
using System.Collections.Generic;
using System.Text;

namespace AStudyInTest.Domain
{
    /// <summary>This exception is thrown when the caller requests an item that doesn't exist in the datastore.</summary>
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Failed to locate the requested resource.")
        {

        }

        public NotFoundException(string message) : base(message)
        {

        }
    }
}
