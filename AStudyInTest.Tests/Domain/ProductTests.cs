using AStudyInTest.Domain.Models;
using AStudyInTest.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace AStudyInTest.Domain
{
    [TestClass]
    public class ProductTests
    {
        /// <summary>Loads up the service using an in memory database.</summary>
        private DatabaseContext GetInMemoryContext()
        {
            // Create in-memory database.
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseInMemoryDatabase(String.Format("Test{0}", Guid.NewGuid()));

            var context = new DatabaseContext(optionsBuilder.Options);
            return context;
        }

        [TestMethod]
        public async Task CreateProductTest()
        {
            // Arrange
            var service = new ProductService(this.GetInMemoryContext());
            var product = new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 9.95M };

            // Act
            await service.CreateAsync(product);

            // Assert
            var result = await service.GetAsync(product.Id);
            Assert.AreEqual(result.Name, product.Name);
            Assert.AreEqual(9.95M, product.Price);
        }
    }
}
