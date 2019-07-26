using AStudyInTest.Domain.Models;
using AStudyInTest.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace AStudyInTest.Domain
{
    [TestClass]
    public class DistributionTests : TestBase
    {
        // As a retailer I would like to be able to define delivery days.
        [TestMethod]
        public async Task CreateDistribution()
        {
            // Arrange
            var service = new DistributionService(this.GetInMemoryContext(), this.GetRetailerUser());
            var day = DateTime.Now.Date;
            var item = new Distribution() { Date = day };

            // Act
            await service.CreateAsync(item);

            // Assert
            var result = await service.GetAsync(item.Id);
            Assert.AreEqual(day, result.Date);
        }
    }
}
