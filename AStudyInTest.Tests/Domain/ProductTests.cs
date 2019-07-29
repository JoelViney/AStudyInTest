using AStudyInTest.Domain.Models;
using AStudyInTest.Domain.Services;
using AStudyInTest.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AStudyInTest.Domain
{
    [TestClass]
    public class ProductTests : TestBase
    {
        // As a customer I would like to be able to see the products that are available.
        [TestMethod]
        public async Task ViewActiveProducts()
        {
            // Arrange
            var databaseContext = this.GetInMemoryContext();
            var service = new ProductService(databaseContext, this.GetRetailerUser());

            await AssureProductExistsAsync(new Product() { Name = $"A_Product_{Guid.NewGuid()}", Price = 10.00M }, databaseContext);
            await AssureProductExistsAsync(new Product() { Name = $"B_Product_{Guid.NewGuid()}", Price = 11.00M }, databaseContext);
            await AssureProductExistsAsync(new Product() { Name = $"C_Product_{Guid.NewGuid()}", Price = 12.00M }, databaseContext);
            await AssureProductExistsAsync(new Product() { Name = $"D_Product_{Guid.NewGuid()}", Price = 13.00M, Active = false }, databaseContext);

            // Act
            var results = await service.GetActiveListAsync();

            // Assert
            Assert.AreEqual(3, results.Count);
            var orderedList = results.OrderBy(x => x.Name);
            Assert.IsTrue(results.SequenceEqual(orderedList), "The results are not ordered.");
        }

        // As a retailer I would like to be able to see all of the products.
        [TestMethod]
        public async Task ViewProducts()
        {
            // Arrange
            var databaseContext = this.GetInMemoryContext();
            var service = new ProductService(databaseContext, this.GetRetailerUser());

            await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 10.00M }, databaseContext);
            await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 11.00M }, databaseContext);
            await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 12.00M }, databaseContext);
            await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 13.00M, Active = false }, databaseContext);

            // Act
            var results = await service.GetListAsync();

            // Assert
            Assert.AreEqual(4, results.Count);
        }

        // As a retailer I would like to be able to define the products that I sell.
        [TestMethod]
        public async Task CreateProduct()
        {
            // Arrange
            var service = new ProductService(this.GetInMemoryContext(), this.GetRetailerUser());
            var product = new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 9.95M };

            // Act
            await service.CreateAsync(product);

            // Assert
            var result = await service.GetAsync(product.Id);
            Assert.AreEqual(result.Name, product.Name);
            Assert.AreEqual(9.95M, product.Price);
            Assert.AreEqual(true, product.Active);
        }

        // As a retailer I would like to be able to update the products that I sell.
        // As a retailer I need to be able to de-activate a product so customers can no longer order them.
        [TestMethod]
        public async Task UpdateProduct()
        {
            // Arrange
            var service = new ProductService(this.GetInMemoryContext(), this.GetRetailerUser());
            var product = new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 9.95M };
            await service.CreateAsync(product);

            var revisedProduct = await service.GetAsync(product.Id);

            // Act
            revisedProduct.Name = $"Product_{Guid.NewGuid()}";
            await service.UpdateAsync(revisedProduct);

            // Assert
            var result = await service.GetAsync(revisedProduct.Id);
            Assert.AreEqual(result.Name, revisedProduct.Name);
            Assert.AreEqual(9.95M, product.Price);
            Assert.AreEqual(true, product.Active);
        }

        // As a retailer I would like to be able to delete my products that I created by mistake.
        [TestMethod]
        public async Task DeleteProduct()
        {
            // Arrange
            var service = new ProductService(this.GetInMemoryContext(), this.GetRetailerUser());
            var product = new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 9.95M };
            await service.CreateAsync(product);

            // Act
            await service.DeleteAsync(product);

            // Assert
            var result = await service.FindAsync(product.Id);
            Assert.IsNull(result);
        }


        // As a retailer I would like to be able to delete my products but only if they haven't been added to an order.
        [TestMethod]
        [ExpectedException(typeof(MethodNotAllowedException))]
        public async Task DeleteProductInAnOrder()
        {
            // Arrange
            var databaseContext = this.GetInMemoryContext();

            var deliveryDay = await AssureDeliveryDayExistsAsync(new DeliveryDay() { Date = DateHelper.Tomorrow, LastOrderDateTime = DateHelper.Today.EndOfDay() }, databaseContext);
            var customer = await AssureCustomerExistsAsync(new Customer() { Name = $"Customer_{Guid.NewGuid()}" }, databaseContext);
            var product = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 10.00M }, databaseContext);

            var orderService = new OrderService(databaseContext, this.GetCustomerUser(customer.Id));

            var order = new Order() { Customer = customer, DeliveryDay = deliveryDay };
            order.Lines.Add(new OrderLine() { Quantity = 2, Product = product });
            await orderService.CreateAsync(order);

            // Act
            var productService = new ProductService(databaseContext, this.GetRetailerUser());

            // Act
            await productService.DeleteAsync(product);

            // Assert - Exception
        }
    }
}
