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
        [TestMethod]
        public async Task CreateProductTest()
        {
            // Arrange
            var service = new ProductService(DatabaseHelper.GetInMemoryContext());
            var product = new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 9.95M };

            // Act
            await service.CreateAsync(product);

            // Assert
            var result = await service.GetAsync(product.Id);
            Assert.AreEqual(result.Name, product.Name);
            Assert.AreEqual(9.95M, product.Price);
            Assert.AreEqual(true, product.Active);
        }

        [TestMethod]
        public async Task DeactivateProductTest()
        {
            // Arrange
            var service = new ProductService(DatabaseHelper.GetInMemoryContext());
            var product = new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 9.95M };
            await service.CreateAsync(product);

            // Act
            product.Active = false;

            // Assert
            var result = await service.GetAsync(product.Id);
            Assert.AreEqual(false, product.Active);
        }
    }
}
