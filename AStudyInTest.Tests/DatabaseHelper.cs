using AStudyInTest.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace AStudyInTest
{
    internal static class DatabaseHelper
    {
        /// <summary>Loads up the service using an in memory database.</summary>
        internal static DatabaseContext GetInMemoryContext()
        {
            // Create in-memory database.
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseInMemoryDatabase(String.Format("Test{0}", Guid.NewGuid()));

            var context = new DatabaseContext(optionsBuilder.Options);
            return context;
        }
    }
}
