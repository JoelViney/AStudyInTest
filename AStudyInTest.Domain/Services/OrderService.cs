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
                throw new Exception($"Orders have closed for the delivery on {"dddd":item.Distribution.LastOrderDateTime} the {DateTimeHelper.GetDaySuffix(item.Distribution.LastOrderDateTime)} of {"MMMM":item.Distribution.LastOrderDateTime}.");
            }

            await base.CreateAsync(item);
        }

    }
}
