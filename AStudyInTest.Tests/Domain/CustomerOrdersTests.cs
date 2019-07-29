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
    public class CustomerOrderTests : TestBase
    {
        // As a customer I would like to be able to place an order for delivery on one of the delivery days.
        [TestMethod]
        public async Task CreateOrder()
        {
            // Arrange
            var databaseContext = this.GetInMemoryContext();

            var customer = await AssureCustomerExistsAsync(new Customer() { Name = $"Customer_{Guid.NewGuid()}" }, databaseContext);
            var product = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 10.00M }, databaseContext);
            var deliveryDayId = await AssureDeliveryDayExistsAsync(new DeliveryDay() { Date = DateHelper.Tomorrow, LastOrderDateTime = DateHelper.Today.EndOfDay() }, databaseContext);

            var service = new OrderService(databaseContext, this.GetCustomerUser(customer.Id));

            var order = new Order() { Customer = customer, DeliveryDay = deliveryDayId };
            order.Lines.Add(new OrderLine() { Quantity = 2, Product = product });

            // Act
            await service.CreateAsync(order);

            // Assert
            var result = await service.GetAsync(order.Id);
            Assert.AreEqual(1, result.Lines.Count);
            Assert.AreEqual(20.00M, result.GetTotal());
        }

        // As a customer I would like to be able to change my order before the last order time has passed.
        [TestMethod]
        public async Task UpdateOrder()
        {
            // Arrange
            var databaseContext = this.GetInMemoryContext();

            var customer = await AssureCustomerExistsAsync(new Customer() { Name = $"Customer_{Guid.NewGuid()}" }, databaseContext);
            var product = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 10.00M }, databaseContext);
            var product2 = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 12.00M }, databaseContext);
            var deliveryDayId = await AssureDeliveryDayExistsAsync(new DeliveryDay() { Date = DateHelper.Tomorrow, LastOrderDateTime = DateHelper.Today.EndOfDay() }, databaseContext);

            var service = new OrderService(databaseContext, this.GetCustomerUser(customer.Id));
            var order = new Order() { Customer = customer, DeliveryDay = deliveryDayId };
            order.Lines.Add(new OrderLine() { Quantity = 2, Product = product });
            await service.CreateAsync(order);

            var updatedOrder = await service.GetAsync(order.Id);
            updatedOrder.Lines.Add(new OrderLine() { Quantity = 1, Product = product2 });

            // Act
            await service.UpdateAsync(order);

            // Assert
            var result = await service.GetAsync(order.Id);
            Assert.AreEqual(2, result.Lines.Count);
            Assert.AreEqual(32.00M, result.GetTotal());
        }

        // As a retailer I would like to be able to close orders for a delivery day a certain time before the event.
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CreateOrderAfterLastOrderDateTime()
        {
            // Arrange
            var databaseContext = this.GetInMemoryContext();

            var customer = await AssureCustomerExistsAsync(new Customer() { Name = $"Customer_{Guid.NewGuid()}" }, databaseContext);
            var product = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}" }, databaseContext);
            var deliveryDay = await AssureDeliveryDayExistsAsync(new DeliveryDay() { Date = DateHelper.Today, LastOrderDateTime = DateHelper.Yesterday }, databaseContext);

            var service = new OrderService(databaseContext, this.GetCustomerUser(customer.Id));

            var order = new Order() { Customer = customer, DeliveryDay = deliveryDay };
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
            var databaseContext = this.GetInMemoryContext();

            var deliveryDay = await AssureDeliveryDayExistsAsync(new DeliveryDay() { Date = DateHelper.Today, LastOrderDateTime = DateHelper.Yesterday }, databaseContext);
            var customer = await AssureCustomerExistsAsync(new Customer() { Name = $"Customer_{Guid.NewGuid()}" }, databaseContext);
            var product = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Active = false }, databaseContext);

            var service = new OrderService(databaseContext, this.GetCustomerUser(customer.Id));

            var order = new Order() { Customer = customer, DeliveryDay = deliveryDay };
            order.Lines.Add(new OrderLine() { Quantity = 1, Product = product });
            await service.CreateAsync(order);

            // Act
            order.Status = OrderStatus.Cancelled;
            await service.UpdateAsync(order);

            // Assert 
            var result =  await service.GetAsync(order.Id);

            Assert.AreEqual(OrderStatus.Cancelled, result.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CancelOrderLine()
        {
            // Arrange
            var databaseContext = this.GetInMemoryContext();
            
            var deliveryDay = await AssureDeliveryDayExistsAsync(new DeliveryDay() { Date = DateHelper.Today, LastOrderDateTime = DateHelper.Yesterday }, databaseContext);
            var customer = await AssureCustomerExistsAsync(new Customer() { Name = $"Customer_{Guid.NewGuid()}" }, databaseContext);
            var product = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Active = false }, databaseContext);

            var service = new OrderService(databaseContext, this.GetCustomerUser(customer.Id));

            var order = new Order() { Customer = customer, DeliveryDay = deliveryDay };
            order.Lines.Add(new OrderLine() { Quantity = 1, Product = product });
            await service.CreateAsync(order);

            // Act
            order.Lines.First().Cancelled = true;
            await service.UpdateAsync(order);

            // Assert 
            var result = await service.GetAsync(order.Id);

            Assert.IsTrue(result.Lines.First().Cancelled);
            Assert.AreEqual(0.00M, result.GetTotal());
        }

        // As a retailer I would like it so that if a product is deactivated it cannot be added to any orders.
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CreateOrderWithDeactivatedProduct()
        {
            // Arrange
            var databaseContext = this.GetInMemoryContext();

            var deliveryDay = await AssureDeliveryDayExistsAsync(new DeliveryDay() { Date = DateHelper.Today, LastOrderDateTime = DateHelper.Yesterday }, databaseContext);
            var customer = await AssureCustomerExistsAsync(new Customer() { Name = $"Customer_{Guid.NewGuid()}" }, databaseContext);
            var product = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Active = false }, databaseContext);

            var service = new OrderService(databaseContext, this.GetCustomerUser(customer.Id));

            var order = new Order() { Customer = customer, DeliveryDay = deliveryDay };
            order.Lines.Add(new OrderLine() { Quantity = 1, Product = product });

            // Act
            await service.CreateAsync(order);

            // Assert - Exception
        }
    }
}
