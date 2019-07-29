using AStudyInTest.Domain.Models;
using AStudyInTest.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using AStudyInTest.Helpers;

namespace AStudyInTest.Domain
{
    [TestClass]
    public class DelieveryDayTests : TestBase
    {
        // As a retailer I would like to be able to define delivery days.
        // As a retailer I would like to be able to define when the customer needs to have their order in by.
        [TestMethod]
        public async Task CreateDeliveryDay()
        {
            // Arrange
            var service = new DeliveryDayService(this.GetInMemoryContext(), this.GetRetailerUser());
            var now = DateTime.Now;
            var deliveryDay = new DeliveryDay()
            {
                LastOrderDateTime = now.Date.Next(DayOfWeek.Wednesday).AtNoon(),
                Date = now.Date.Next(DayOfWeek.Friday).StartOfDay(),
            };

            // Act
            await service.CreateAsync(deliveryDay);

            // Assert
            var result = await service.GetAsync(deliveryDay.Id);
            Assert.AreEqual(now.Date.Next(DayOfWeek.Wednesday).AtNoon(), result.LastOrderDateTime);
            Assert.AreEqual(now.Date.Next(DayOfWeek.Friday).StartOfDay(), result.Date);
        }

        // As a retailer I would like to be able to update a delivery day.
        [TestMethod]
        public async Task UpdateDeliveryDay()
        {
            // Arrange
            var now = DateTime.Now;
            var service = new DeliveryDayService(this.GetInMemoryContext(), this.GetRetailerUser());
            var deliveryDay = new DeliveryDay()
            {
                LastOrderDateTime = now.Next(DayOfWeek.Wednesday).AtNoon(),
                Date = now.Next(DayOfWeek.Friday).StartOfDay(),
            };
            await service.CreateAsync(deliveryDay);

            var revisedDeliveryDay = await service.GetAsync(deliveryDay.Id);

            // Act
            revisedDeliveryDay.Date = now.Next(DayOfWeek.Saturday).StartOfDay();
            await service.UpdateAsync(revisedDeliveryDay);

            // Assert
            var result = await service.GetAsync(deliveryDay.Id);
            Assert.AreEqual(now.Next(DayOfWeek.Saturday).StartOfDay(), result.Date);
        }
    }
}
