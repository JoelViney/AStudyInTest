using AStudyInTest.Domain.Helpers;
using AStudyInTest.Domain.Models;
using System;
using System.Threading.Tasks;

namespace AStudyInTest.Domain.Services
{
    public class OrderService : ServiceBase<Order>
    {
        private ITimeService _timeService;

        public OrderService(DatabaseContext databaseContext) : this(databaseContext, new TimeService())
        {

        }

        public OrderService(DatabaseContext databaseContext, ITimeService timeService) : base(databaseContext)
        {
            _timeService = timeService;
        }


        public override async Task CreateAsync(Order item)
        {
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

            // Assure that the price is right.
            foreach (var line in item.Lines)
            {
                line.Amount = line.Product.Price;
            }

            await base.CreateAsync(item);
        }

        public async Task UpdateAsync(Order item)
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
