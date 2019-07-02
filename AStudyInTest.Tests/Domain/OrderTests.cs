using AStudyInTest.Domain.Models;
using AStudyInTest.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace AStudyInTest.Domain
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public async Task CreateOrder()
        {
            // Arrange
            var service = new OrderService(DatabaseHelper.GetInMemoryContext());
            var distributionService = new DistributionService(DatabaseHelper.GetInMemoryContext());
            var customerService = new CustomerService(DatabaseHelper.GetInMemoryContext());
            var productService = new ProductService(DatabaseHelper.GetInMemoryContext());

            var distribution = new Distribution() { Date = DateTime.Now.Date };
            await distributionService.CreateAsync(distribution);
            var customer = new Customer() { Name = $"Customer_{Guid.NewGuid()}" };
            await customerService.CreateAsync(customer);
            var product = new Product() { Name = $"Product_{Guid.NewGuid()}" };
            await productService.CreateAsync(product);

            var order = new Order() { Customer = customer, Distribution = distribution };
            order.Lines.Add(new OrderLine() { Quantity = 1, Product = product });

            // Act
            await service.CreateAsync(order);

            // Assert
            var result = await service.GetAsync(order.Id);
            Assert.AreEqual(1, result.Lines.Count);
        }

    }
}
