using AStudyInTest.Domain.Models;
using AStudyInTest.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using AStudyInTest.Helpers;

namespace AStudyInTest.Domain
{
    [TestClass]
    public class OrderTests
    {
        private async Task<Distribution> AssureDistributionExistsAsync(Distribution item)
        {
            var service = new DistributionService(DatabaseHelper.GetInMemoryContext());
            await service.CreateAsync(item);
            return item;
        }

        private async Task<Customer> AssureCustomerExistsAsync(Customer item)
        {
            var service = new CustomerService(DatabaseHelper.GetInMemoryContext());
            await service.CreateAsync(item);
            return item;
        }

        private async Task<Product> AssureProductExistsAsync(Product item)
        {
            var service = new ProductService(DatabaseHelper.GetInMemoryContext());
            await service.CreateAsync(item);
            return item;
        }

        [TestMethod]
        public async Task CreateOrder()
        {
            // Arrange
            var service = new OrderService(DatabaseHelper.GetInMemoryContext());

            var distribution = await AssureDistributionExistsAsync(new Distribution() { Date = DateHelper.Tomorrow, LastOrderDateTime = DateHelper.Today });
            var customer = await AssureCustomerExistsAsync(new Customer() { Name = $"Customer_{Guid.NewGuid()}" });
            var product = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}" });

            var order = new Order() { Customer = customer, Distribution = distribution };
            order.Lines.Add(new OrderLine() { Quantity = 1, Product = product });

            // Act
            await service.CreateAsync(order);

            // Assert
            var result = await service.GetAsync(order.Id);
            Assert.AreEqual(1, result.Lines.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CreateOrderAfterLastOrderDateTime()
        {
            // Arrange
            var timeServiceMock = new Mock<ITimeService>();
            timeServiceMock.Setup(x => x.GetDateTimeNow()).Returns(DateHelper.Today);

            var service = new OrderService(DatabaseHelper.GetInMemoryContext(), timeServiceMock.Object);

            var distribution = await AssureDistributionExistsAsync(new Distribution() { Date = DateHelper.Today, LastOrderDateTime = DateHelper.Yesterday });
            var customer = await AssureCustomerExistsAsync(new Customer() { Name = $"Customer_{Guid.NewGuid()}" });
            var product = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}" });

            var order = new Order() { Customer = customer, Distribution = distribution };
            order.Lines.Add(new OrderLine() { Quantity = 1, Product = product });

            // Act
            await service.CreateAsync(order);

            // Assert - Exception
        }
    }
}
