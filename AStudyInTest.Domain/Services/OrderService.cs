using AStudyInTest.Domain.Helpers;
using AStudyInTest.Domain.Models;
using System;
using System.Threading.Tasks;

namespace AStudyInTest.Domain.Services
{
    public class OrderService : ServiceBase<Order>
    {
        private readonly ITimeService _timeService;

        public OrderService(DatabaseContext databaseContext, ICurrentUser currentUser) : this(databaseContext, currentUser, new TimeService())
        {

        }

        public OrderService(DatabaseContext databaseContext, ICurrentUser currentUser, ITimeService timeService) : base(databaseContext, currentUser)
        {
            _timeService = timeService;
        }


        public override async Task CreateAsync(Order item)
        {
            // Validation
            if (_timeService.GetDateTimeNow() > item.Distribution.LastOrderDateTime)
            {
                throw new Exception($"Orders have closed for the delivery on {item.Distribution.LastOrderDateTime:dddd} the {DateTimeHelper.GetDaySuffix(item.Distribution.LastOrderDateTime)} of {item.Distribution.LastOrderDateTime:MMMM}.");
            }

            foreach (var line in item.Lines)
            {
                if (!line.Product.Active)
                {
                    throw new Exception($"The product '{line.Product.Price}' is no longer active in the inventory.");
                }
            }

            // Set the price at time of order creation purchase.
            foreach (var line in item.Lines)
            {
                line.Amount = line.Product.Price;
            }

            await base.CreateAsync(item);
        }

        public override async Task UpdateAsync(Order item)
        {
            foreach (var line in item.Lines)
            {
                if (line.IsNew())
                {
                    base.DatabaseContext.Add(line);
                }
            }

            await base.DatabaseContext.SaveChangesAsync();
        }

    }
}
