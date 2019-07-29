using AStudyInTest.Domain.Models;
using AStudyInTest.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace AStudyInTest.Domain
{
    [TestClass]
    public class CustomerTests : TestBase
    {
        [TestMethod]
        public async Task CreateCustomer()
        {
            // Arrange
            var service = new CustomerService(this.GetInMemoryContext(), null);
            var item = new Customer() { Name = $"Customer_{Guid.NewGuid()}" };

            // Act
            await service.CreateAsync(item);

            // Assert
            var result = await service.GetAsync(item.Id);

            Assert.AreEqual(item.Name, result.Name);
        }
    }
}
