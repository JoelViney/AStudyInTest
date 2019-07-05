using AStudyInTest.Domain.Models;
using AStudyInTest.Domain.Services;
using AStudyInTest.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AStudyInTest.Domain
{
    [TestClass]
    public class OrderTests : TestBase
    {
        [TestMethod]
        public async Task CreateOrder()
        {
            // Arrange
            var databaseContext = DatabaseHelper.GetInMemoryContext();

            var distribution = await AssureDistributionExistsAsync(new Distribution() { Date = DateHelper.Tomorrow, LastOrderDateTime = DateHelper.Today.EndOfDay() }, databaseContext);
            var customer = await AssureCustomerExistsAsync(new Customer() { Name = $"Customer_{Guid.NewGuid()}" }, databaseContext);
            var product = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 10.00M }, databaseContext);

            var service = new OrderService(databaseContext, this.GetCustomerUser(customer.Id));

            var order = new Order() { Customer = customer, Distribution = distribution };
            order.Lines.Add(new OrderLine() { Quantity = 2, Product = product });

            // Act
            await service.CreateAsync(order);

            // Assert
            var result = await service.GetAsync(order.Id);
            Assert.AreEqual(1, result.Lines.Count);
            Assert.AreEqual(20.00M, result.Total);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CreateOrderAfterLastOrderDateTime()
        {
            // Arrange
            var databaseContext = DatabaseHelper.GetInMemoryContext();

            var distribution = await AssureDistributionExistsAsync(new Distribution() { Date = DateHelper.Today, LastOrderDateTime = DateHelper.Yesterday }, databaseContext);
            var customer = await AssureCustomerExistsAsync(new Customer() { Name = $"Customer_{Guid.NewGuid()}" }, databaseContext);
            var product = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}" }, databaseContext);

            var service = new OrderService(databaseContext, this.GetCustomerUser(customer.Id));

            var order = new Order() { Customer = customer, Distribution = distribution };
            order.Lines.Add(new OrderLine() { Quantity = 1, Product = product });

            // Act
            await service.CreateAsync(order);

            // Assert - Exception
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CreateOrderWithDeactivatedProduct()
        {
            // Arrange
            var databaseContext = DatabaseHelper.GetInMemoryContext();
            
            var distribution = await AssureDistributionExistsAsync(new Distribution() { Date = DateHelper.Today, LastOrderDateTime = DateHelper.Yesterday }, databaseContext);
            var customer = await AssureCustomerExistsAsync(new Customer() { Name = $"Customer_{Guid.NewGuid()}" }, databaseContext);
            var product = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Active = false }, databaseContext);

            var service = new OrderService(databaseContext, this.GetCustomerUser(customer.Id));

            var order = new Order() { Customer = customer, Distribution = distribution };
            order.Lines.Add(new OrderLine() { Quantity = 1, Product = product });

            // Act
            await service.CreateAsync(order);

            // Assert - Exception
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CancelOrder()
        {
            // Arrange
            var databaseContext = DatabaseHelper.GetInMemoryContext();

            var distribution = await AssureDistributionExistsAsync(new Distribution() { Date = DateHelper.Today, LastOrderDateTime = DateHelper.Yesterday }, databaseContext);
            var customer = await AssureCustomerExistsAsync(new Customer() { Name = $"Customer_{Guid.NewGuid()}" }, databaseContext);
            var product = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Active = false }, databaseContext);

            var service = new OrderService(databaseContext, this.GetCustomerUser(customer.Id));

            var order = new Order() { Customer = customer, Distribution = distribution };
            order.Lines.Add(new OrderLine() { Quantity = 1, Product = product });
            await service.CreateAsync(order);

            // Act
            order.Cancelled = true;
            await service.UpdateAsync(order);

            // Assert 
            var result =  await service.GetAsync(order.Id);

            Assert.IsTrue(result.Cancelled);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CancelOrderLine()
        {
            // Arrange
            var databaseContext = DatabaseHelper.GetInMemoryContext();
            
            var distribution = await AssureDistributionExistsAsync(new Distribution() { Date = DateHelper.Today, LastOrderDateTime = DateHelper.Yesterday }, databaseContext);
            var customer = await AssureCustomerExistsAsync(new Customer() { Name = $"Customer_{Guid.NewGuid()}" }, databaseContext);
            var product = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Active = false }, databaseContext);

            var service = new OrderService(databaseContext, this.GetCustomerUser(customer.Id));

            var order = new Order() { Customer = customer, Distribution = distribution };
            order.Lines.Add(new OrderLine() { Quantity = 1, Product = product });
            await service.CreateAsync(order);

            // Act
            order.Lines.First().Cancelled = true;
            await service.UpdateAsync(order);

            // Assert 
            var result = await service.GetAsync(order.Id);

            Assert.IsTrue(result.Lines.First().Cancelled);
            Assert.AreEqual(0.00M, result.Total);
        }

    }
}
