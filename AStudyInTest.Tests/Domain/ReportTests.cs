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
    public class ReportTests : TestBase
    {
        [TestMethod]
        public async Task PickingListReport()
        {
            // Arrange
            var databaseContext = this.GetInMemoryContext();
            var service = new ReportService(databaseContext);

            var distribution = await AssureDistributionExistsAsync(new Distribution() { Date = DateHelper.Tomorrow, LastOrderDateTime = DateHelper.Today.EndOfDay() }, databaseContext);
            var customer = await AssureCustomerExistsAsync(new Customer() { Name = $"Customer_{Guid.NewGuid()}" }, databaseContext);
            var productA = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 10.00M }, databaseContext);
            var productB = await AssureProductExistsAsync(new Product() { Name = $"Product_{Guid.NewGuid()}", Price = 5.00M }, databaseContext);

            var order1 = new Order() { Customer = customer, Distribution = distribution };
            order1.Lines.Add(new OrderLine() { Quantity = 2, Product = productA });

            var order2 = new Order() { Customer = customer, Distribution = distribution };
            order2.Lines.Add(new OrderLine() { Quantity = 1, Product = productB});

            await AssureOrderExistsAsync(order1, databaseContext);
            await AssureOrderExistsAsync(order2, databaseContext);

            // Act
            var report = await service.GetPickingListAsync(distribution.Id);

            Assert.AreEqual(2, report.Lines.Count);
            Assert.AreEqual(2, report.OrderCount);
            Assert.AreEqual(2, report.Lines.First(x => x.ProductId == productA.Id).Quantity);
            Assert.AreEqual(1, report.Lines.First(x => x.ProductId == productB.Id).Quantity);
        }
    }
}
