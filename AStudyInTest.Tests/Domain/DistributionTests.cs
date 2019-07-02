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
    public class DistributionTests
    {
        [TestMethod]
        public async Task CreateDistributionTest()
        {
            // Arrange
            var service = new DistributionService(DatabaseHelper.GetInMemoryContext());
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
