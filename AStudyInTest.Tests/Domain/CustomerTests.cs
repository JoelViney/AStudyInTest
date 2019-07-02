using AStudyInTest.Domain.Models;
using AStudyInTest.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AStudyInTest.Domain
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public async Task CreateCustomerTest()
        {
            // Arrange
            var service = new CustomerService(DatabaseHelper.GetInMemoryContext());
            var item = new Customer() { Name = $"Customer_{Guid.NewGuid()}" };

            // Act
            await service.CreateAsync(item);

            // Assert
            var result = await service.GetAsync(item.Id);

            Assert.AreEqual(item.Name, result.Name);
        }

    }
}
